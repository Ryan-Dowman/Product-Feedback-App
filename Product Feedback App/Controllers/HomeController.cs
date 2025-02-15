using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.View;
using Product_Feedback_App.Respositories;
using System.Diagnostics;

namespace Product_Feedback_App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedbackRepository feedbackRepository;

        public HomeController(ILogger<HomeController> logger, IFeedbackRepository feedbackRepository)
        {
            _logger = logger;
            this.feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Feedback> feedback = await feedbackRepository.GetAllFeedbackAsync();
            return View(feedback);
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
