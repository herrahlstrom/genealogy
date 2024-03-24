using System;
using System.Diagnostics;
using System.Linq;
using Genealogy.Application;
using Microsoft.Extensions.Caching.Memory;

namespace Genealogy.Infrastructure;

internal class CacheManager : ICache
{
    private readonly IMemoryCache _cache;
    public CacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<TResult> GetOrCreateAsync<TKey, TResult>(TKey key, Func<CancellationToken, Task<TResult>> valueFactory, TimeSpan cacheLifetime, CancellationToken cancellationToken = default)
        where TKey : CacheKey
    {
        return await _cache.GetOrCreateAsync(key, InternalFactory) ?? throw new UnreachableException($"{nameof(valueFactory)} does not produce a value");

        Task<TResult> InternalFactory(ICacheEntry entry)
        {
            entry.AbsoluteExpirationRelativeToNow = cacheLifetime;
            return valueFactory.Invoke(cancellationToken);
        }
    }
}
