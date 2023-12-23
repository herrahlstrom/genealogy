using Genealogy.Application;
using Genealogy.Infrastructure.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Genealogy.Api.Auth;

internal class ConfigureJwtBearerOptions(IOptions<AuthTokenOptions> options, IKeyStore keyStore) : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthTokenOptions _options = options.Value;

    public void Configure(JwtBearerOptions options)
    {
        throw new NotImplementedException();
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuers = [_options.Issuer.AbsoluteUri],

            // ToDo: Fix
            ValidateIssuerSigningKey = false,
            RequireSignedTokens = false,
            IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) => keyStore.GetKeys(kid),

            RequireAudience = false,
            ValidateAudience = false,

            ClockSkew = TimeSpan.FromSeconds(30)
        };
    }
}