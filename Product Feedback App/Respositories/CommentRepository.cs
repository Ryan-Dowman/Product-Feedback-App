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

            // Need to carefully handle User when being placed into another entity instance
            comment.User = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == comment.User.Id);

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

        public async Task<Comment?> GetCommentByIdAsync(Guid id)
        {
            return await appDbContext.Comments.Include(comment => comment.User).Include(comment => comment.Replies).ThenInclude(comment => comment.User).FirstOrDefaultAsync(currentComment => currentComment.Id == id);
        }

        public async Task<Comment?> UpdateCommentAsync(Comment comment)
        {
            Comment? targetComment = await appDbContext.Comments.FirstOrDefaultAsync(currentComment => currentComment.Id == comment.Id);

            if (targetComment == null) return null;

            targetComment.Id = comment.Id;
            targetComment.FeedbackId = comment.FeedbackId;
            targetComment.User = comment.User;
            targetComment.Content = comment.Content;
            targetComment.TargetUsername = comment.TargetUsername;
            targetComment.ParentComment = comment.ParentComment;
            targetComment.ParentCommentId = comment.ParentCommentId;
            targetComment.Replies = comment.Replies;

            await appDbContext.SaveChangesAsync();

            return comment;
        }
    }
}
