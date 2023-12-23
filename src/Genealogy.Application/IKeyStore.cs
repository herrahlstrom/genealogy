using Microsoft.IdentityModel.Tokens;

namespace Genealogy.Application;

public interface IKeyStore
{
    IEnumerable<SecurityKey> GetKeys(string kid);
    Task<SecurityKey> GetActiveKey(CancellationToken cancellationToken);
}