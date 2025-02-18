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
        private readonly ICommentRepository commentRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository, UserManager<AppUser> userManager, ICommentRepository commentRepository)
        {
            this.feedbackRepository = feedbackRepository;
            this.userManager = userManager;
            this.commentRepository = commentRepository;
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

                Status = Status.Suggestion,

                Details = feedbackCreateViewModel.Details,

                Upvotes = new List<Upvote>(),

                Comments = new List<Comment>()
            };

            await feedbackRepository.CreateFeedbackAsync(feedback);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {   
            Feedback? feedback = await feedbackRepository.GetFeedbackByIdAsync(id);

            if (feedback == null) return View(null);
            
            bool userHasUpvoted = feedback.Upvotes.FirstOrDefault(upvote => upvote.UserId == Guid.Parse(userManager.GetUserId(User))) != null;

            FeedbackViewViewModel feedbackViewViewModel = new FeedbackViewViewModel
            {
                Feedback = feedback,
                UserHasUpvoted = userHasUpvoted
            };

            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View("View", feedbackViewViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Feedback? feedback = await feedbackRepository.GetFeedbackByIdAsync(id);

            if (feedback == null) return RedirectToAction("Index", "Home");

            FeedbackEditViewModel feedbackEditViewModel = new FeedbackEditViewModel
            {
                Id = feedback.Id,
                Title = feedback.Title,
                Category = feedback.Category,
                Details = feedback.Details
            };

            return View(feedbackEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FeedbackEditViewModel feedbackEditViewModel)
        {
            if (!ModelState.IsValid) return View();

            Feedback feedback = new Feedback
            {
                Id = feedbackEditViewModel.Id,
                Title = feedbackEditViewModel.Title,
                Category = feedbackEditViewModel.Category,
                Details = feedbackEditViewModel.Details
            };

            await feedbackRepository.UpdateFeedbackAsync(feedback);

            return RedirectToAction("View", "Feedback", new { id = feedbackEditViewModel.Id });
        }

        [HttpPost("api/feedback/edit/status/{feedbackId:Guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid feedbackId, [FromBody]string statusName)
        {
            Feedback? feedback = await feedbackRepository.GetFeedbackByIdAsync(feedbackId);

            if (feedback == null) return BadRequest("Feedback could not be found");

            if (!Enum.TryParse(statusName, true, out Status newStatus)) return BadRequest("Feedback could not be found");

            Feedback newFeedback = new Feedback
            {
                Id = feedback.Id,
                UserId = feedback.UserId,
                Title = feedback.Title,
                Category = feedback.Category,
                Status = newStatus,
                Details = feedback.Details,
                Upvotes = feedback.Upvotes,
                Comments = feedback.Comments,
            };

            await feedbackRepository.UpdateFeedbackAsync(newFeedback);


            return Ok("Status Update has been registered");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            Feedback? feedback = await feedbackRepository.GetFeedbackByIdAsync(id);

            if (feedback != null) await feedbackRepository.DeleteFeedbackById(feedback.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}
