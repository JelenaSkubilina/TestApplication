using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using WebSite.Core.Helpers;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Data");
        }

        [Authorize(Roles = "admin")]
        public IActionResult About()
        {
            return Content("only for admin");
        }
    }
}
