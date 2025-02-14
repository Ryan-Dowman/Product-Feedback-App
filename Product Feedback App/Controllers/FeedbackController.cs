using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.Enums;
using Product_Feedback_App.Models.Identity;
using Product_Feedback_App.Models.View;
using Product_Feedback_App.Respositories;

namespace Product_Feedback_App.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly UserManager<AppUser> userManager;

        public FeedbackController(IFeedbackRepository feedbackRepository, UserManager<AppUser> userManager)
        {
            this.feedbackRepository = feedbackRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackCreateViewModel feedbackCreateViewModel)
        {
            if (!ModelState.IsValid) return View();

            bool parseCategoryResult = Enum.TryParse<Category>(feedbackCreateViewModel.Category, true, out Category category);

            Feedback feedback = new Feedback
            {
                UserId = Guid.Parse(userManager.GetUserId(User)),

                Title = feedbackCreateViewModel.Title,

                Category = parseCategoryResult ? category : Category.Bug,

                Status = Status.InProgress,

                Details = feedbackCreateViewModel.Details,

                Upvotes = new List<Upvote>(),

                Comments = new List<Comment>()
            };

            await feedbackRepository.CreateFeedbackAsync(feedback);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult View(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult View(FeedbackViewViewModel feedbackViewViewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(FeedbackEditViewModel feedbackEditViewModel)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
