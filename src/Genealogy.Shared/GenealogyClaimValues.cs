using System;
using System.Linq;
using System.Security.Claims;

namespace Genealogy;

public static class GenealogyClaimTypes
{
    public const string PersonId = "PersonId";
    public const string Role = ClaimTypes.Role;
}

public static class GenealogyClaimRoles
{
    public const string Reader = "Reader";
    public const string Admin = "Admin";
}
