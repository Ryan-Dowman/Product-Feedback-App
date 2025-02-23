using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.Identity;
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
        private readonly UserManager<AppUser> userManager;

        public HomeController(ILogger<HomeController> logger, IFeedbackRepository feedbackRepository, UserManager<AppUser> userManager)
        {
            _logger = logger;
            this.feedbackRepository = feedbackRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? orderBy)
        {
            List<Feedback> allFeedback = await feedbackRepository.GetAllFeedbackAsync();

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.Equals("Least Upvotes")) allFeedback = allFeedback.OrderBy(feedback => feedback.Upvotes.Count).ToList();
                else if (orderBy.Equals("Least Comments")) allFeedback = allFeedback.OrderByDescending(feedback => feedback.Comments.Count).ToList();
                else if (orderBy.Equals("Most Comments")) allFeedback = allFeedback.OrderBy(feedback => feedback.Comments.Count).ToList();
                else allFeedback = allFeedback.OrderByDescending(feedback => feedback.Upvotes.Count).ToList();
            }

            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            homeIndexViewModel.FeedbackViewModels = new();

            foreach (Feedback feedback in allFeedback)
            {
                homeIndexViewModel.FeedbackViewModels.Add(
                   new FeedbackViewModel()
                   {
                       Feedback = feedback,
                       UserHasUpvoted = feedback.Upvotes.FirstOrDefault(upvote => upvote.UserId == Guid.Parse(userManager.GetUserId(User))) != null
                   }     
                );
            }

            return View(homeIndexViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Roadmap()
        {
            List<Feedback> allFeedback = await feedbackRepository.GetAllFeedbackAsync();

            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            homeIndexViewModel.FeedbackViewModels = new();

            foreach (Feedback feedback in allFeedback)
            {
                homeIndexViewModel.FeedbackViewModels.Add(
                   new FeedbackViewModel()
                   {
                       Feedback = feedback,
                       UserHasUpvoted = feedback.Upvotes.FirstOrDefault(upvote => upvote.UserId == Guid.Parse(userManager.GetUserId(User))) != null
                   }
                );
            }

            return View(homeIndexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
