using System;
using System.Linq;

namespace Genealogy.Application;

public interface ICache
{
    public Task<TResult?> GetOrCreateAsync<TKey, TResult>(TKey key, Func<CancellationToken, Task<TResult>> valueFactory, TimeSpan cacheLifetime, CancellationToken cancellationToken = default)
         where TKey : CacheKey;
}

public abstract record CacheKey();