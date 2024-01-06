using Genealogy.Domain.Entities.Auth;
using Genealogy.Domain.Exceptions;
using Genealogy.Web.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Genealogy.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Genealogy.Web.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Genealogy.Web.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest request, [FromServices] IAuthService authService)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        User user;
        try
        {
            user = await authService.Login(request.Username, request.Password);
        }
        catch (NotFoundException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(new LoginRequest() { Username = request.Username });
        }
        catch (UnauthorizedAccessException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(new LoginRequest() { Username = request.Username });
        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected error during login; {Message}", ex.Message);
            ModelState.AddModelError("", "Unknown error");
            return View(new LoginRequest() { Username = request.Username });
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Username),
            new(ClaimTypes.Name, user.Name),
        };

        if(user.PersonId != null)
        {
            claims.Add(new Claim(GenealogyClaimTypes.PersonId, $"{user.PersonId:D}"));
        }

        claims.AddRange(await authService.GetAuthClaims(user.Id));


        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties { IsPersistent = true });

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
