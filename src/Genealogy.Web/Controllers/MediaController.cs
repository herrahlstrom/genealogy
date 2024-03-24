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
    private readonly IMediaHandler _mediaHandler;
    private readonly MediaOptions _options;

    public MediaController(GenealogyDbContext dbContext, ICache cache, IMediaHandler mediaHandler, IOptions<MediaOptions> options)
    {
        _mediaHandler = mediaHandler;
        _dbContext = dbContext;
        _cache = cache;
        _options = options.Value;
    }

    /// <param name="filename">Is only used for the url</param>
    [HttpGet("{id:guid}/{filename}")]
    [Authorize]
    public async Task<IActionResult> Media(Guid id, [FromRoute(Name = "filename")] string filename)
    {
        try
        {
            MediaData data = await GetMedia(id);
            var fullPath = Path.Combine(_options.BasePath, data.Path);
            return CreateFileResult(fullPath);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }


    /// <param name="filename">Is only used for the url</param>
    [HttpGet("{id:guid}/{maxWidth:int}x{maxHeight:int}/{filename}")]
    [Authorize]
    public async Task<IActionResult> Thumbnail(Guid id, [FromRoute(Name = "maxWidth")] int maxWidth, [FromRoute(Name = "maxHeight")] int maxHeight, [FromRoute(Name = "filename")] string filename)
    {
        try
        {
            MediaData data = await GetMedia(id);

            var sourceFullPath = Path.Combine(_options.BasePath, data.Path);
            var targetFullPath = Path.Combine(Path.GetTempPath(), $"{Path.GetFileNameWithoutExtension(data.Path)}_{maxWidth}x{maxHeight}{Path.GetExtension(data.Path)}");

            if (!System.IO.File.Exists(targetFullPath))
            {
                await _mediaHandler.ResizeImage(sourceFullPath, targetFullPath, new(maxWidth, maxHeight));
            }

            return CreateFileResult(targetFullPath);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    private PhysicalFileResult CreateFileResult(string fullPath)
    {
        if (new FileExtensionContentTypeProvider().TryGetContentType(fullPath, out var contentType) == false)
        {
            contentType = "application/octet-stream";
        }

        return PhysicalFile(fullPath, contentType);
    }

    private async Task<MediaData> GetMedia(Guid id)
    {
        return await _cache.GetOrCreateAsync(new MediaDataCacheKey(id), Factory, TimeSpan.FromMinutes(15)) ?? throw MediaNotFoundException.Create(id);

        Task<MediaData?> Factory(CancellationToken ct) => GetMediaQuery(id).SingleOrDefaultAsync(ct);
        IQueryable<MediaData> GetMediaQuery(Guid id) => from media in _dbContext.Media
                                                        where media.Id == id
                                                        select new MediaData(media.Id, media.Path);
    }

    private record MediaDataCacheKey(Guid Id) : CacheKey;
    private record MediaData(Guid Id, string Path);
}
