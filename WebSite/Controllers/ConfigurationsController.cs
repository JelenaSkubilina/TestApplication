using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    public class ConfigurationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
