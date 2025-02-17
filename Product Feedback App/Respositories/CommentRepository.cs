using Microsoft.EntityFrameworkCore;
using Product_Feedback_App.Data;
using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.Identity;
using System.Diagnostics;

namespace Product_Feedback_App.Respositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext appDbContext;

        public CommentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {

            // Need to carefully handle user when being placed into another entity instance
            // Check if the User is already being tracked by the DbContext
            AppUser existingUser = await appDbContext.Users
                                                  .FirstOrDefaultAsync(u => u.Id == comment.User.Id);

            if (existingUser != null)
            {
                // If the user is already tracked, use the existing instance
                comment.User = existingUser;
            }
            else
            {
                // If the user is not tracked, mark the comment's user as Unchanged
                appDbContext.Entry(comment.User).State = EntityState.Unchanged;
            }

            await appDbContext.Comments.AddAsync(comment);
            await appDbContext.SaveChangesAsync();

            return comment;

        }

        public async Task<Comment?> DeleteCommentAsync(Comment comment)
        {
            Comment? targetComment = await appDbContext.Comments.FirstOrDefaultAsync(currentComment => currentComment.Id == comment.Id);

            if (targetComment == null) return null;

            appDbContext.Comments.Remove(targetComment);
            await appDbContext.SaveChangesAsync();

            return comment;
        }
    }
}
