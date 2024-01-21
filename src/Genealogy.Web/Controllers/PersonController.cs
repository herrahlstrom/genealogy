using System.Diagnostics;
using System.Linq;
using Genealogy.Api.Auth;
using Genealogy.Domain.Entities;
using Genealogy.Infrastructure.Data;
using Genealogy.Shared.Exceptions;
using Genealogy.Web.Components.Search.Models;
using Genealogy.Web.Models.Person;
using Genealogy.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Api.Controllers;

[Route("persons")]
[Authorize(Policies.CanRead)]
public class PersonController(IDbContextFactory<GenealogyDbContext> dbContextFactory) : Controller
{
    [HttpGet("api/search")]
    public async Task<IActionResult> ApiSearch([FromQuery(Name = "q")] string? queryString)
    {
        if (string.IsNullOrWhiteSpace(queryString))
        {
            return BadRequest();
        }

        using var dbContext = dbContextFactory.CreateDbContext();
        IQueryable<PersonEntity> query = dbContext.Persons;
        foreach (var textPart in queryString.ToLower().Split())
        {
            query = query.Where(x => x.Name.ToLower().Contains(textPart));
        }

        var itemsQuery = from x in query
                         select new
                         {
                             x.Id,
                             x.Name,
                             BirthDate = (from e in x.Events
                                          where e.Event.Type == EventType.Födelse
                                          select e.Date ?? e.Event.Date).FirstOrDefault()
                         };

        var itemsData = await itemsQuery.ToListAsync();

        var items = new List<SearchResultItem>(itemsData.Count);
        foreach (var item in itemsData.OrderByDescending(x => x.BirthDate))
        {
            items.Add(new SearchResultItem
            {
                Id = item.Id,
                Name = new PersonName(item.Name),
                BirthDate = new DateModel(item.BirthDate),
                Url = Url.Action<PersonController>(nameof(Person), values: new { id = item.Id })
            });
        }

        return Ok(new SearchResult
        {
            Items = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Person(Guid id)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        try
        {
            var person = await (from p in dbContext.Persons
                                where p.Id == id
                                select new
                                {
                                    p.Id,
                                    p.Name,
                                    p.Sex,
                                    p.Profession,
                                    p.Notes
                                }).FirstOrDefaultAsync() ?? throw PersonNotFoundException.Create(id);

            var timelineItems = await GetTimelineItems(id);

            var model = new PersonViewModel
            {
                Id = id,
                Name = new PersonName(person.Name),
                Sex = person.Sex,
                Profession = person.Profession,
                Notes = person.Notes,
                TimelineItems = timelineItems
            };

            return View(model);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
    
    private async Task<List<TimelineItem>> GetTimelineItems(Guid id)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var person = await dbContext.Persons.Where(x => x.Id == id).Include(x => x.Events).ThenInclude(x => x.Event).FirstOrDefaultAsync()
                ?? throw PersonNotFoundException.Create(id);
        DateModel? birthDate = person.Events
                                     .Where(x => x.Event.Type == EventType.Födelse)
                                     .Select(x => x.Date ?? x.Event.Date)
                                     .FirstOrDefault();

        List<TimelineItem> items = [];

        items.AddRange(person.Events
                             .Select(x => new TimelineItem
                             {
                                 EventId = x.EventId,
                                 Name = x.Event.Name ?? x.Event.Type.ToString(),
                                 Type = x.Event.Type,
                                 Date = x.Date ?? x.Event.Date,
                                 Location = x.Event.Location
                             }.SetRelativeAge(birthDate)));

        // Load families when parent
        await dbContext.Entry(person).Collection(x => x.Families).LoadAsync();
        foreach (var familyMember in person.Families)
        {
            List<Tuple<string, Uri>>? links = null;

            FamilyMember? partner = null;
            if (familyMember is FamilyParentMember)
            {
                await dbContext.Entry(familyMember)
                               .Reference(x => x.Family)
                               .Query()
                               .Include(x => x.Events)
                               .ThenInclude(x => x.Event)
                               .LoadAsync();
                var family = familyMember.Family;

                if (family.Events.Count == 0)
                {
                    continue;
                }

                await dbContext.Entry(family).Collection(x => x.FamilyMembers).LoadAsync();
                partner = family.FamilyMembers
                                .Where(x => x.MemberType == FamilyMemberType.Parent)
                                .Where(x => x.PersonId != id)
                                .SingleOrDefault();
                if (partner is not null)
                {
                    await dbContext.Entry(partner).Reference(x => x.Person).LoadAsync();
                    var partnerName = new PersonName(partner.Person.Name);
                    var partnerUrl = Url.Action(nameof(Person), new { id = partner.PersonId })! ?? throw new UnreachableException();
                    links ??= [];
                    links.Add(Tuple.Create(partnerName.DisplayName, new Uri(partnerUrl, UriKind.RelativeOrAbsolute)));
                }

                items.AddRange(family.Events
                                     .Select(x => new TimelineItem
                                     {
                                         EventId = x.EventId,
                                         Name = x.Event.Name ?? x.Event.Type.GetDisplayName(),
                                         Type = x.Event.Type,
                                         Date = x.Date ?? x.Event.Date,
                                         Location = x.Event.Location,
                                         Links = links
                                     }.SetRelativeAge(birthDate)));
            }
        }

        items.Sort(new TimelineItemSorter());
        return items;
    }

    private class TimelineItemSorter : IComparer<TimelineItem>
    {
        public int Compare(TimelineItem? x, TimelineItem? y)
        {
            if (x?.Date is null && y?.Date is null)
                return 0;
            if (x?.Date is null)
                return -1;
            if (y?.Date is null)
                return 1;

            var n = x.Date.CompareTo(y.Date);
            if (n != 0)
                return n;
            return x.Name.CompareTo(y.Name);
        }
    }
}
