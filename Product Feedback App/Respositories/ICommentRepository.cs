using Product_Feedback_App.Models.Domain;

namespace Product_Feedback_App.Respositories
{
    public interface ICommentRepository
    {
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment?> DeleteCommentAsync(Comment comment);
        Task<Comment?> GetCommentByIdAsync(Guid id);
        Task<Comment?> UpdateCommentAsync(Comment comment);
    }
}
