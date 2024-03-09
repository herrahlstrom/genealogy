using System.Diagnostics;
using Genealogy.Api.Auth;
using Genealogy.Domain.Entities;
using Genealogy.Infrastructure.Data;
using Genealogy.Shared.Exceptions;
using Genealogy.Web.Components.Search.Models;
using Genealogy.Web.Models.Media;
using Genealogy.Web.Models.Person;
using Genealogy.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Web.Controllers;

[Route("persons")]
[Authorize(Policies.CanRead)]
public class PersonController : Controller
{
    private readonly ICacheControl _cache;
    private readonly GenealogyDbContext _dbContext;

    public PersonController(GenealogyDbContext dbContext, ICacheControl cache)
    {
        _cache = cache;
        _dbContext = dbContext;
    }

    [HttpGet("api/search")]
    public async Task<IActionResult> ApiSearch([FromQuery(Name = "q")] string? queryString)
    {
        if (string.IsNullOrWhiteSpace(queryString))
        {
            return BadRequest();
        }

        IQueryable<PersonEntity> query = _dbContext.Persons;
        foreach (var textPart in queryString.ToLower().Split())
        {
            query = query.Where(x => x.Name.ToLower().Contains(textPart));
        }

        var itemsQuery = from x in query
                         select new
                         {
                             x.Id,
                             x.Name
                         };

        var itemsData = await itemsQuery.ToListAsync();

        var items = new List<SearchResultItem>(itemsData.Count);
        foreach (var item in itemsData)
        {
            items.Add(new SearchResultItem
            {
                Id = item.Id,
                Name = new PersonName(item.Name),
                BirthDate = await GetBirthDate(item.Id),
                Url = GetPersonUrl(item.Id)
            });
        }

        return Ok(new SearchResult
        {
            Items = items.OrderByDescending(x => x.BirthDate).ToList()
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Person(Guid id)
    {
        try
        {
            var person = await (from p in _dbContext.Persons
                                where p.Id == id
                                select new
                                {
                                    p.Id,
                                    Name = new PersonName(p.Name),
                                    p.Sex,
                                    p.Profession,
                                    p.Notes,
                                    Media = p.Media.OrderBy(x=> x.Title).Select(x => new { x.Id, x.Title, x.Path, x.Notes}).ToList()
                                }).FirstOrDefaultAsync() ?? throw PersonNotFoundException.Create(id);

            var timelineItems = await GetTimelineItems(id);
            var tree = await GetPersonTree(new TreePerson(
                Id: person.Id,
                Name: person.Name,
                Sex: person.Sex,
                BirthDate: await GetBirthDate(person.Id),
                DeathDate: await GetDeathDate(person.Id)));

            var model = new PersonViewModel
            {
                Id = id,
                Name = person.Name,
                Sex = person.Sex,
                Profession = person.Profession,
                Notes = person.Notes,
                TimelineItems = timelineItems,
                Tree = tree,
                Media = person.Media.Select(m => new MediaViewModel
                {
                    Id = m.Id,
                    Title = m.Title ?? "",
                    Url = Url.Action(controller: "Media", action: "Media", values: new { m.Id, filename = m.Title }) ?? throw new UnreachableException(),
                    Notes = m.Notes ?? ""
                }).ToList(),
            };

            return View(model);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    private async Task<DateModel> GetBirthDate(Guid personId)
    {
        var dictionary = await _cache.GetOrCreateAsync(
            key: new BirthDateCacheKey(),
            cacheLifetime: TimeSpan.FromMinutes(30),
            valueFactory: ct => (from p in _dbContext.Persons
                                 from pe in p.Events
                                 where pe.Event.Type == EventType.Födelse
                                 select new
                                 {
                                     PersonId = p.Id,
                                     Date = pe.Date ?? pe.Event.Date
                                 }).ToDictionaryAsync(x => x.PersonId, x => x.Date, cancellationToken: ct));

        if (dictionary.TryGetValue(personId, out string? date))
        {
            return new DateModel(date);
        }
        return DateModel.Empty;
    }

    private async Task<DateModel> GetDeathDate(Guid personId)
    {
        var dictionary = await _cache.GetOrCreateAsync(
            key: new DeathDatesCacheKey(),
            cacheLifetime: TimeSpan.FromMinutes(30),
            valueFactory: ct => (from p in _dbContext.Persons
                                 from pe in p.Events
                                 where pe.Event.Type == EventType.Död
                                 select new
                                 {
                                     PersonId = p.Id,
                                     Date = pe.Date ?? pe.Event.Date
                                 }).ToDictionaryAsync(x => x.PersonId, x => x.Date, cancellationToken: ct));

        if (dictionary.TryGetValue(personId, out string? date))
        {
            return new DateModel(date);
        }
        return DateModel.Empty;
    }

    private async Task<List<PersonTreeFamily>> GetFamilies(Guid id)
    {
        var familiesAsParent = await (from f in _dbContext.Families
                                      where f.FamilyMembers.Any(m => m.PersonId == id && m.MemberType == FamilyMemberType.Parent)
                                      select f.Id).ToListAsync();

        var partners = await (from fm in _dbContext.FamilyMembers
                              where familiesAsParent.Contains(fm.FamilyId)
                              where fm.MemberType == FamilyMemberType.Parent
                              where fm.PersonId != id
                              select new
                              {
                                  fm.FamilyId,
                                  Partner = new { fm.Person.Id, fm.Person.Name, fm.Person.Sex },
                              }).ToDictionaryAsync(x => x.FamilyId, x => x.Partner);

        var children = await (from fm in _dbContext.FamilyMembers
                              where familiesAsParent.Contains(fm.FamilyId)
                              where fm.MemberType != FamilyMemberType.Parent
                              select new
                              {
                                  fm.FamilyId,
                                  Child = new { fm.Person.Id, fm.Person.Name, fm.Person.Sex },
                                  ChildType = fm.MemberType
                              }).ToListAsync();

        List<PersonTreeFamily> result = [];
        foreach (var familyId in familiesAsParent)
        {
            TreePerson? partner = null;
            if (partners.TryGetValue(familyId, out var partnerData))
            {
                partner = new TreePerson(
                    Id: partnerData.Id,
                    Name: new PersonName(partnerData.Name),
                    Sex: partnerData.Sex,
                    BirthDate: await GetBirthDate(partnerData.Id),
                    DeathDate: await GetDeathDate(partnerData.Id));
            }

            List<TreePerson> familyChildren = [];
            List<TreePerson>? familyFosterChildren = null;
            foreach (var child in children.Where(y => y.FamilyId == familyId))
            {
                var childTreePerson = new TreePerson(
                    Id: child.Child.Id,
                    Name: new PersonName(child.Child.Name),
                    Sex: child.Child.Sex,
                    BirthDate: await GetBirthDate(child.Child.Id),
                    DeathDate: await GetDeathDate(child.Child.Id));

                switch (child.ChildType)
                {
                    case FamilyMemberType.Child:
                        familyChildren.Add(childTreePerson);
                        break;

                    case FamilyMemberType.FosterChild:
                        familyFosterChildren ??= [];
                        familyFosterChildren.Add(childTreePerson);
                        break;

                    default: throw new NotSupportedException($"Invalid type of children: '{child.ChildType}'");
                }
            }
            familyChildren.Sort((a, b) => a.BirthDate.CompareTo(b.BirthDate));
            familyFosterChildren?.Sort((a, b) => a.BirthDate.CompareTo(b.BirthDate));

            result.Add(new PersonTreeFamily(partner, familyChildren, familyFosterChildren));
        }

        result.Sort((a, b) => (a.Children.Count > 0, b.Children.Count > 0) switch
        {
            (true, true) => a.Children.First().BirthDate.CompareTo(b.Children.First().BirthDate),
            (true, false) => -1,
            (false, true) => 1,
            (false, false) => 0,
        });
        return result;
    }

    private async Task<Guid?> GetParentId(Guid id, PersonSex sex)
    {
        var parentId = await _cache.GetOrCreateAsync(
            new GetParentCacheKey(id, sex),
            cacheLifetime: TimeSpan.FromMinutes(30),
            valueFactory: ct => (from member in _dbContext.FamilyMembers
                                 where member.MemberType == FamilyMemberType.Parent
                                 where member.Person.Sex == sex
                                 where member.Family.FamilyMembers.Any(x => x.PersonId == id && x.MemberType == FamilyMemberType.Child)
                                 select member.PersonId).SingleOrDefaultAsync());
        if (parentId == Guid.Empty)
            return null;
        return parentId;
    }

    private async Task<ParentPersonTreeNode> GetParentPersonTreeNode(Guid id, int withGrandParentsLevels)
    {
        var p = await _cache.GetOrCreateAsync(new ParentPersonTreeNodeCacheKey(id),
        cacheLifetime: TimeSpan.FromMinutes(15),
        valueFactory: ct => (from person in _dbContext.Persons
                             where person.Id == id
                             select new { person.Name, person.Sex }).SingleAsync(ct));

        ParentPersonTreeNode? grandFather = null;
        ParentPersonTreeNode? grandMother = null;

        if (withGrandParentsLevels > 0)
        {
            var fatherId = await GetParentId(id, PersonSex.Male);
            if (fatherId.HasValue)
            {
                grandFather = await GetParentPersonTreeNode(fatherId.Value, withGrandParentsLevels - 1);
            }

            var motherId = await GetParentId(id, PersonSex.Female);
            if (motherId.HasValue)
            {
                grandMother = await GetParentPersonTreeNode(motherId.Value, withGrandParentsLevels - 1);
            }
        }

        return new ParentPersonTreeNode(id, new PersonName(p.Name), p.Sex, grandFather, grandMother, await GetBirthDate(id), await GetDeathDate(id));
    }

    private async Task<PersonTree> GetPersonTree(TreePerson self)
    {
        var fatherId = await GetParentId(self.Id, PersonSex.Male);
        var motherId = await GetParentId(self.Id, PersonSex.Female);

        return new PersonTree()
        {
            Self = self,
            Father = fatherId.HasValue ? await GetParentPersonTreeNode(fatherId.Value, 1) : null,
            Mother = motherId.HasValue ? await GetParentPersonTreeNode(motherId.Value, 1) : null,
            Families = await GetFamilies(self.Id)
        };
    }

    private Uri GetPersonUrl(Guid id)
    {
        var pathToAction = Url.Action<PersonController>(nameof(Person), new { id }) ?? throw new UnreachableException();
        return new Uri(pathToAction, UriKind.RelativeOrAbsolute);
    }
    private Uri GetSourceUrl(Guid id)
    {
        var pathToAction = Url.Action<SourceController>(nameof(SourceController.Source), new { id }) ?? throw new UnreachableException();
        return new Uri(pathToAction, UriKind.RelativeOrAbsolute);
    }
    private Uri GetEventUrl(Guid id)
    {
        var pathToAction = Url.Action<EventController>(nameof(EventController.Event), new { id }) ?? throw new UnreachableException();
        return new Uri(pathToAction, UriKind.RelativeOrAbsolute);
    }

    private async Task<List<TimelineItem>> GetTimelineItems(Guid id)
    {
        var q = _dbContext.Persons
                          .Where(x => x.Id == id)
                          .Include(x => x.Events)
                          .ThenInclude(x => x.Event)
                          .ThenInclude(x => x.Sources);

        var sql = q.ToQueryString();

        var person = await q
                                     .FirstOrDefaultAsync()
                ?? throw PersonNotFoundException.Create(id);
        DateModel birthDate = await GetBirthDate(person.Id);

        List<TimelineItem> items = [];

        foreach (var pEvent in person.Events)
        {
            List<ILink> links = [.. pEvent.Event.Sources.Select(x => new SourceReference(x.Name, GetSourceUrl(x.Id)))];

            var item = new TimelineItem
            {
                EventId = pEvent.EventId,
                Name = pEvent.Event.Name ?? pEvent.Event.Type.ToString(),
                Type = pEvent.Event.Type,
                Date = pEvent.Date ?? pEvent.Event.Date,
                Location = pEvent.Event.Location,
                Links = links
            }.SetRelativeAge(birthDate);
            items.Add(item);
        }


        // Load families when parent
        await _dbContext.Entry(person).Collection(x => x.Families).LoadAsync();
        foreach (var familyMember in person.Families.OfType<FamilyParentMember>())
        {
            // Load family with the events
            await _dbContext.Entry(familyMember).Reference(x => x.Family).Query().Include(x => x.Events).ThenInclude(x => x.Event).ThenInclude(x => x.Sources).LoadAsync();
            var family = familyMember.Family;

            if (family.Events.Count == 0)
            {
                continue;
            }

            List<ILink> familyLinks = new();

            // Load all family members to get an eventual partner
            await _dbContext.Entry(family).Collection(x => x.FamilyMembers).LoadAsync();
            FamilyMember? partner = family.FamilyMembers.OfType<FamilyParentMember>().Where(x => x.PersonId != id).SingleOrDefault();

            if (partner is not null)
            {
                // Load partner to get name
                await _dbContext.Entry(partner).Reference(x => x.Person).LoadAsync();
                familyLinks.Add(new PersonReference(
                    Name: new PersonName(partner.Person.Name),
                    Url: GetPersonUrl(partner.PersonId)));
            }

            foreach (var fEvent in family.Events)
            {
                var eventLinks = new List<ILink>(familyLinks);
                eventLinks.AddRange(fEvent.Event.Sources.Select(x => new SourceReference(x.Name, GetSourceUrl(x.Id))));

                var item = new TimelineItem
                {
                    EventId = fEvent.EventId,
                    Name = fEvent.Event.Name ?? fEvent.Event.Type.GetDisplayName(),
                    Type = fEvent.Event.Type,
                    Date = fEvent.Date ?? fEvent.Event.Date,
                    Location = fEvent.Event.Location,
                    Links = eventLinks
                }.SetRelativeAge(birthDate);
                items.Add(item);
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

    private record BirthDateCacheKey() : CacheKey;
    private record DeathDatesCacheKey() : CacheKey;
    private record GetParentCacheKey(Guid Id, PersonSex Sex) : CacheKey;
    private record ParentPersonTreeNodeCacheKey(Guid Id) : CacheKey;
}
