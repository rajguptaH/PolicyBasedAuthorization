using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyBasedAuthorization.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PolicyBasedAuthorization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            var name = HttpContext.User.FindFirst(c=>c.Type == ClaimTypes.Name).Value;
            ViewBag.Name = name;
            return View();
        }
        [Authorize(Policy = "AtLeast30K")]
        public IActionResult Privacy()
        {
            var name = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            var salary = HttpContext.User.FindFirst(c => c.Type == "Salary").Value;
            ViewBag.Name = name;
            ViewBag.Salary = salary;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}