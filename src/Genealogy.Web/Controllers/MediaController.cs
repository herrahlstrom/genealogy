using Genealogy.Application;
using Genealogy.Infrastructure.Data;
using Genealogy.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Genealogy.Web;

[Route("media")]
public class MediaController : Controller
{
    private readonly ICache _cache;
    private readonly GenealogyDbContext _dbContext;
    private readonly MediaOptions _options;

    public MediaController(GenealogyDbContext dbContext, ICache cache, IOptions<MediaOptions> options)
    {
        _dbContext = dbContext;
        _cache = cache;
        _options = options.Value;
    }

    /// <param name="filename">Is only used for the url</param>
    [HttpGet("{id}/{filename}")]
    [Authorize]
    public async Task<IActionResult> Media(Guid id, string filename)
    {
        try
        {
            var data = await _cache.GetOrCreateAsync(
                key: new MediaDataCacheKey(id),
                cacheLifetime: TimeSpan.FromMinutes(15),
                valueFactory: ct => (from media in _dbContext.Media
                                     where media.Id == id
                                     select new { media.Id, media.Path }).SingleOrDefaultAsync(ct)) ?? throw MediaNotFoundException.Create(id);

            var fullPath = Path.Combine(_options.BasePath, data.Path);

            if (new FileExtensionContentTypeProvider().TryGetContentType(data.Path, out var contentType) == false)
            {
                contentType = "application/octet-stream";
            }

            return PhysicalFile(fullPath, contentType);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    private record MediaDataCacheKey(Guid Id) : CacheKey;
}
