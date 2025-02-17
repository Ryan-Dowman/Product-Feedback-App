using Microsoft.EntityFrameworkCore;
using Product_Feedback_App.Data;
using Product_Feedback_App.Models.Domain;

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
