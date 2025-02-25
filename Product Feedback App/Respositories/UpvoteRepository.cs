using Microsoft.EntityFrameworkCore;
using Product_Feedback_App.Data;
using Product_Feedback_App.Models.Domain;

namespace Product_Feedback_App.Respositories
{
    public class UpvoteRepository : IUpvoteRepository
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly AppDbContext appDbContext;

        public UpvoteRepository(IFeedbackRepository feedbackRepository, AppDbContext appDbContext)
        {
            this.feedbackRepository = feedbackRepository;
            this.appDbContext = appDbContext;
        }

        public async Task<int?> GetFeedbackUpvoteCount(Guid feedbackId)
        {
            Feedback? feedback = await feedbackRepository.GetFeedbackByIdAsync(feedbackId);

            if (feedback == null) return null;

            return feedback.Upvotes.Count;
        }

        private async Task<Upvote> CreateUpvoteAsync(Upvote upvote)
        {
            await appDbContext.Upvotes.AddAsync(upvote);
            await appDbContext.SaveChangesAsync();

            return upvote;
        }

        private async Task<Upvote> RemoveUpvoteAsync(Upvote upvote)
        {
            Upvote? targetUpvote = await appDbContext.Upvotes.FirstOrDefaultAsync(x => x.UserId == upvote.UserId && x.FeedbackId == upvote.FeedbackId);

            if (targetUpvote != null)
            {
                appDbContext.Upvotes.Remove(targetUpvote);
                await appDbContext.SaveChangesAsync();
            }

            return upvote;
        }

        async Task<Upvote> IUpvoteRepository.SubmitUpvoteAsync(Upvote upvote)
        {
            Upvote? existingUpvote = await appDbContext.Upvotes.FirstOrDefaultAsync(x => x.UserId == upvote.UserId && x.FeedbackId == upvote.FeedbackId);

            if (existingUpvote != null)
            {
                await RemoveUpvoteAsync(upvote);
            }
            else
            {
                await CreateUpvoteAsync(upvote);
            }

            return upvote;
        }
    }
}
