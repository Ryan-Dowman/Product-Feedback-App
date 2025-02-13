using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.View;
using System.Diagnostics;

namespace Product_Feedback_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<Feedback>());
        }

        [HttpGet]
        public IActionResult Roadmap()
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
