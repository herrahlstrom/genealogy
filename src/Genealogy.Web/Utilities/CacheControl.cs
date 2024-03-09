using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace Genealogy.Web;

public interface ICacheControl
{
    Task<TValue> GetOrCreateAsync<TKey, TValue>(TKey key, Func<CancellationToken, Task<TValue>> valueFactory, TimeSpan cacheLifetime, CancellationToken ct = default) where TKey : CacheKey;
}

public abstract record CacheKey();

internal class CacheControl(IMemoryCache cache) : ICacheControl
{
    public async Task<TValue> GetOrCreateAsync<TKey, TValue>(TKey key, Func<CancellationToken, Task<TValue>> valueFactory, TimeSpan cacheLifetime, CancellationToken ct = default) where TKey : CacheKey
    {
        return await cache.GetOrCreateAsync(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = cacheLifetime;
            return valueFactory.Invoke(ct);
        }) ?? throw new UnreachableException($"{nameof(valueFactory)} does not produce a value");
    }
}
