using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tide.Models;

namespace Tide.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult User()
        {
            return View();
        }

        public IActionResult Place()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
