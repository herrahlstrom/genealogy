using System.Security.Cryptography;
using Genealogy.Application;
using Genealogy.Domain.Data.Auth;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Genealogy.Infrastructure.Application;

internal class KeyStore : IKeyStore
{
    private static readonly string ActiveCachekey = $"{Guid.NewGuid():N}";
    private static readonly string ValidCachekey = $"{Guid.NewGuid():N}";
    private static TimeSpan GenerateKeyInAdvance = TimeSpan.FromDays(7);
    private static TimeSpan KeyActiveLifetime = TimeSpan.FromDays(1);
    private static TimeSpan KeyValidLifetime = TimeSpan.FromDays(28);
    private readonly IMemoryCache _cache;

    private readonly IDbContextFactory<GenealogyDbContext> _dbContextFactory;
    private readonly ILogger<KeyStore> _logger;
    private readonly ITimeService _timeService;

    public KeyStore(IDbContextFactory<GenealogyDbContext> dbContextFactory, ITimeService timeService, IMemoryCache cache, ILogger<KeyStore> logger)
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
        _timeService = timeService;
        _cache = cache;
    }

    public static RSAParameters DeserializePem(TextReader reader)
    {
        AsymmetricCipherKeyPair KeyPair;
        using (var pr = new PemReader(reader))
        {
            KeyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
        }
        return DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);
    }

    public static void SerializeToPem(RSAParameters p, TextWriter writer)
    {
        using var pw = new PemWriter(writer);
        pw.WriteObject(DotNetUtilities.GetRsaKeyPair(p));
    }

    public async Task<SecurityKey> GetActiveKey(CancellationToken cancellationToken)
    {
        var activeKeys = await GetActiveKeys(cancellationToken);

        if (activeKeys.Count == 0)
        {
            await EnsureActiveKeysExists(cancellationToken);
            activeKeys = await GetActiveKeys(cancellationToken);
        }

        return activeKeys.OrderByDescending(x => x.ActiveFrom)
                            .Select(CreateSecurityKey)
                            .FirstOrDefault() ?? throw new InvalidOperationException("Can't find any active keys in database");
    }

    public IEnumerable<SecurityKey> GetKeys(string kid)
    {
        return GetValidKeys().Where(x => x.Id == kid).Select(CreateSecurityKey);
    }

    private static RsaSecurityKey CreateSecurityKey(RsaKey rsaKey)
    {
        RSAParameters parameters;
        using (var reader = new StringReader(rsaKey.PemKey))
        {
            parameters = DeserializePem(reader);
        }

        return new RsaSecurityKey(parameters)
        {
            KeyId = rsaKey.Id
        };
    }

    private async Task EnsureActiveKeysExists(CancellationToken cancellationToken)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        var keys = await dbContext.RsaKeys.Where(x => x.ActiveTo > _timeService.UtcNow).ToListAsync(cancellationToken);
        List<RsaKey> generatedKeys = [];

        DateTime point = _timeService.UtcNow;
        DateTime until = _timeService.UtcNow + GenerateKeyInAdvance;
        while (point < until)
        {
            var k = keys.Where(x => x.IsActive(point)).ToList();
            if (k.Count > 0)
            {
                var next = k.Max(x => x.ActiveTo);
                if (next > point)
                {
                    point = next;
                    continue;
                }
            }

            string pem;
            using (var pemWriter = new StringWriter())
            {
                using var rsa = RSA.Create();
                SerializeToPem(rsa.ExportParameters(true), pemWriter);
                pem = pemWriter.ToString();
            }

            var key = new RsaKey()
            {
                Id = $"{Guid.NewGuid():N}",
                Created = _timeService.UtcNow,
                ValidFrom = point,
                ValidTo = point + KeyValidLifetime,
                ActiveFrom = point,
                ActiveTo = point + KeyActiveLifetime,
                PemKey = pem
            };

            dbContext.RsaKeys.Add(key);
            await dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("New key generated: {Id}. The key will be active until {ActiveTo} and valid until {ValidTo}", key.Id, key.ActiveTo, key.ValidTo);

            point = key.ActiveTo;

            keys.Add(key);
            generatedKeys.Add(key);
        }

        _cache.Remove(ActiveCachekey);
        _cache.Remove(ValidCachekey);
    }

    private async Task<IReadOnlyCollection<RsaKey>> GetActiveKeys(CancellationToken cancellationToken)
    {
        var now = _timeService.UtcNow;

        var keys = await _cache.GetOrCreateAsync<IReadOnlyCollection<RsaKey>>(ActiveCachekey, async entry =>
                   {
                       using var dbContext = _dbContextFactory.CreateDbContext();
                       return await dbContext.RsaKeys
                                             .Where(x => x.ActiveTo >= now)
                                             .ToListAsync(cancellationToken);
                   }) ?? throw new System.Diagnostics.UnreachableException();

        return keys.Where(rsaKey => rsaKey.IsActive(now)).ToList();
    }

    private IReadOnlyCollection<RsaKey> GetValidKeys()
    {
        var now = _timeService.UtcNow;

        var keys = _cache.GetOrCreate<IReadOnlyCollection<RsaKey>>(ValidCachekey, entry =>
                   {
                       using var dbContext = _dbContextFactory.CreateDbContext();
                       return dbContext.RsaKeys
                                             .Where(x => x.ValidTo >= now)
                                             .ToList();
                   }) ?? throw new System.Diagnostics.UnreachableException();

        return keys.Where(rsaKey => rsaKey.IsValid(now)).ToList();
    }
}