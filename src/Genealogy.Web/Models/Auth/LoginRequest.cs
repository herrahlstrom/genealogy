using System.ComponentModel.DataAnnotations;

namespace Genealogy.Web.Models.Auth;

public class LoginRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; } = "";

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = "";
}
