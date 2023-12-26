using Genealogy.Api.Auth;
using Genealogy.Domain.Entities;
using Genealogy.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Api.Controllers;

[Route("persons")]
[Authorize(Policies.CanRead)]
public class PersonController(IDbContextFactory<GenealogyDbContext> dbContextFactory) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var person = dbContext.Persons.Where(x => x.Id == id).FirstOrDefault();
        if (person == null)
        {
            return NotFound();
        }

        throw new NotImplementedException();
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetAsync([FromQuery(Name = "q")] string queryString)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        IQueryable<PersonEntity> query = dbContext.Persons;
        foreach (var textPart in queryString.ToLower().Split())
        {
            query = query.Where(x => x.Name.ToLower().Contains(textPart));
        }

        throw new NotImplementedException();
    }
}
