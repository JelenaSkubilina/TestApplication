using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class ConfigurationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
