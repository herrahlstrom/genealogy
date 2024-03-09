using Genealogy.Api.Auth;
using Genealogy.Web.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Genealogy.Web.Controllers;

[Route("events")]
[Authorize(Policies.CanRead)]
public class EventController : Controller
{
    [HttpGet("{id}")]
    public IActionResult Event(Guid id)
    {
        var model = new EventViewModel() { Id = id };
        return View(model);
    }
}
