using System.Linq;
using Genealogy.Api.Auth;
using Genealogy.Domain.Entities;
using Genealogy.Infrastructure.Data;
using Genealogy.Web.Components.Search.Models;
using Genealogy.Web.Models.Person;
using Genealogy.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Genealogy.Api.Controllers;

[Route("persons")]
[Authorize(Policies.CanRead)]
public class PersonController(IDbContextFactory<GenealogyDbContext> dbContextFactory) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Person(Guid id)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var person = await (from p in dbContext.Persons
                            where p.Id == id
                            select new
                            {
                                p.Id,
                                p.Name
                            }).FirstOrDefaultAsync();
        if (person == null)
        {
            return NotFound();
        }

        var events = await (from p in dbContext.Persons
                            where p.Id == id
                            from e in p.Events
                            select new
                            {
                                EventId = e.Event.Id,
                                EventType = e.Event.Type,
                                EventName = e.Event.Name,
                                Date = e.Date ?? e.Event.Date,
                                EndDate = e.EndDate ?? e.Event.EndDate,
                                e.Event.Location
                            }).ToListAsync();

        return View(new PersonViewModel
            {
                Id = id,
                Name = new PersonName(person.Name),
                Events =
                    (from e in events
                     select new PersonEventViewModel(e.EventId, e.EventName ?? e.EventType.ToString(), e.Date, e.EndDate, e.Location)).ToList()
            });
    }

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
}
