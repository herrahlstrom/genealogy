using Microsoft.AspNetCore.Mvc;

namespace Genealogy.Web.Controllers;

[Route("")]
public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
