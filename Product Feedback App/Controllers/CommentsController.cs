using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.Identity;
using Product_Feedback_App.Models.View;
using Product_Feedback_App.Respositories;

namespace Product_Feedback_App.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentRepository commentRepository;
        private readonly IFeedbackRepository feedbackRepository;
        private readonly UserManager<AppUser> userManager;

        public CommentsController(ICommentRepository commentRepository, IFeedbackRepository feedbackRepository, UserManager<AppUser> userManager)
        {
            this.commentRepository = commentRepository;
            this.feedbackRepository = feedbackRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackViewViewModel feedbackViewViewModel, Guid feedbackId)
        {
            if (String.IsNullOrEmpty(feedbackViewViewModel.CommentContent))
            {
                TempData["ErrorMessage"] = "Comment cannot be empty";
                return RedirectToAction("View", "Feedback", new { id = feedbackId });
            }

            Feedback? feedback = await feedbackRepository.GetFeedbackByIdAsync(feedbackId);

            if(feedback == null) return RedirectToAction("Index", "Home");

            AppUser user = await userManager.GetUserAsync(User);

            Comment comment = new Comment
            {
                FeedbackId = feedback.Id,
                User = user,
                Content = feedbackViewViewModel.CommentContent,
                TargetUsername = null,
                ParentCommentId = null,
                ParentComment = null,
                Replies = new List<Comment>()

            };

            await commentRepository.CreateCommentAsync(comment);

            return Redirect($"/Feedback/View/{feedbackId}");
        }
    }
}
