using Genealogy.Api.Auth;
using Genealogy.Application;
using Genealogy.Infrastructure.Data;
using Genealogy.Shared.Exceptions;
using Genealogy.Web.Models.Media;
using Genealogy.Web.Models.Sources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Genealogy.Web.Controllers;

[Route("sources")]
[Authorize(Policies.CanRead)]
public class SourceController : Controller
{
    private readonly ICache _cache;
    private readonly GenealogyDbContext _dbContext;

    public SourceController(GenealogyDbContext dbContext, ICache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Source(Guid id)
    {
        try
        {
            var source = await _dbContext.Sources
                .Where(x => x.Id == id)
                .Include(x => x.Media)
                .FirstOrDefaultAsync() ?? throw SourceNotFoundException.Create(id);

            var model = new SourceViewModel()
            {
                Id = id,
                Name = source.Name,
                Repository = source.Repository ?? "",
                Volume = source.Volume ?? "",
                Page = source.Page ?? "",
                Url = source.Url ?? "",
                ReferenceId = source.ReferenceId ?? "",
                Notes = source.Notes,
                Media = source.Media.Select(m => new MediaViewModel
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
}
