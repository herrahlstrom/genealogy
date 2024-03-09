using Genealogy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Genealogy.Web.Controllers;

[Route("development")]
public class DevelopmentController : Controller
{
    private readonly GenealogyDbContext _dbContext;

    public DevelopmentController(GenealogyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("fixDuplicateImages")]
    public async Task<IActionResult> FixOrhpanImagesAsync()
    {
        var persons = await _dbContext.Persons
                                      .Where(x => x.Media.Any())
                                      .Where(x => x.Events.Any())
                                      .Include(x => x.Media)
                                      .Include(x => x.Events)
                                      .ThenInclude(x => x.Event)
                                      .ThenInclude(x => x.Sources)
                                      .ThenInclude(x => x.Media)
                                      .ToListAsync();

        Debugger.Break();

        int mediaRemoved = 0;

        foreach(var person in persons)
        {
            var mediaInSources = person.Events.SelectMany(x => x.Event.Sources).SelectMany(x => x.Media).Select(x => x.Id).ToHashSet();
            var dupMediaOnPerson = person.Media.Where(x => mediaInSources.Contains(x.Id)).ToList();
            foreach(var media in dupMediaOnPerson)
            {
                person.Media.Remove(media);
                mediaRemoved++;
            }
        }
        
        Debugger.Break();

        //var trans = _dbContext.Database.BeginTransaction();
        var result = _dbContext.SaveChanges();
        //trans.Rollback();

        return Ok(result);
    }
}
