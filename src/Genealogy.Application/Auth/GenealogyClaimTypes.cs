namespace Genealogy.Api.Auth;

public static class GenealogyClaimTypes
{
    public const string Permission = "permission";
    public static class Identity
    {
        public const string Sub = "sub";
        public const string Name = "name";
    }
}

public static class PermissionValues
{
    public const string Read = "Read";
    public const string Write = "Write";
    public const string Admin = "Admin";
}
