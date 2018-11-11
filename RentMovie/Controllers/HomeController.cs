using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentMovie.Models;

namespace RentMovie.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", new { Controller = "Movies" });
            }
            else
            {
                return RedirectToAction("Login", "Account",  new { Area = "Identity" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
