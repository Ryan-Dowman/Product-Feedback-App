using Product_Feedback_App.Models.Domain;

namespace Product_Feedback_App.Respositories
{
    public interface IUpvoteRepository
    {
        Task<Upvote> SubmitUpvoteAsync(Upvote upvote);
        Task<int?> GetFeedbackUpvoteCount(Guid feedbackId);
    }
}
