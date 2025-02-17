using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product_Feedback_App.Data;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.Enums;
using Product_Feedback_App.Models.View;

namespace Product_Feedback_App.Respositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext appDbContext;

        public FeedbackRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Feedback> CreateFeedbackAsync(Feedback feedback)
        {
            await appDbContext.Feedback.AddAsync(feedback);
            await appDbContext.SaveChangesAsync();

            return feedback;
        }

        public async Task<Feedback?> DeleteFeedbackById(Guid id)
        {
            Feedback? targetFeedback = await appDbContext.Feedback.FirstOrDefaultAsync(feedback => feedback.Id == id);

            if (targetFeedback == null) return null;
            
            appDbContext.Feedback.Remove(targetFeedback);
            await appDbContext.SaveChangesAsync();

            return targetFeedback;
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            return await appDbContext.Feedback
                                 .Include(feedback => feedback.Upvotes)                     // Include Upvotes
                                     .Include(feedback => feedback.Comments)                // Include Comments again
                                         .ThenInclude(comment => comment.Replies)           // Include Replies for each Comment
                                 .ToListAsync();
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(Guid id)
        {
            Feedback? targetFeedback = await appDbContext.Feedback.Include(feedback => feedback.Upvotes).Include(feedback => feedback.Comments).Include(feedback => feedback.Upvotes)                     // Include Upvotes
                                     .Include(feedback => feedback.Comments)                // Include Comments again
                                         .ThenInclude(comment => comment.Replies).FirstOrDefaultAsync(feedback => feedback.Id == id);

            if (targetFeedback == null) return null;

            return targetFeedback;
        }

        public async Task<Feedback?> UpdateFeedbackAsync(Feedback feedback)
        {
            Feedback? targetFeedback = await appDbContext.Feedback.FirstOrDefaultAsync(currentFeedback => currentFeedback.Id == feedback.Id);

            if (targetFeedback == null) return null;

            targetFeedback.UserId = feedback.UserId;
            targetFeedback.Title = feedback.Title;
            targetFeedback.Category = feedback.Category;
            targetFeedback.Status = feedback.Status;
            targetFeedback.Details = feedback.Details;
            targetFeedback.Upvotes = feedback.Upvotes;
            targetFeedback.Comments = feedback.Comments;

            await appDbContext.SaveChangesAsync();

            return targetFeedback;
        }
    }
}
