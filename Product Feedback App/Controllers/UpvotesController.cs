using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.Identity;
using Product_Feedback_App.Models.View;
using Product_Feedback_App.Respositories;

namespace Product_Feedback_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpvotesController : ControllerBase
    {
        private readonly IUpvoteRepository upvoteRepository;

        public UpvotesController(IUpvoteRepository upvoteRepository)
        {
            this.upvoteRepository = upvoteRepository;
        }

        [HttpGet]
        [Route("{feedbackId:Guid}/Upvotes")]
        public async Task<IActionResult> GetNumberOfUpvotesForFeedback(Guid feedbackId)
        {
            int? upvotesCount = await upvoteRepository.GetFeedbackUpvoteCount(feedbackId);

            return Ok(upvotesCount);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(UpvotesViewModel upvotesViewModel)
        {
            Upvote upvote = new Upvote
            {
                UserId = upvotesViewModel.UserId,
                FeedbackId = upvotesViewModel.FeedbackId
            };

            await upvoteRepository.SubmitUpvoteAsync(upvote);

            return Ok("Upvote registered");
        }
    }
}
