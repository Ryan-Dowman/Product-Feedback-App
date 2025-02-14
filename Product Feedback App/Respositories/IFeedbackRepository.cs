using Product_Feedback_App.Models.Domain;

namespace Product_Feedback_App.Respositories
{
    public interface IFeedbackRepository
    {
        Task<Feedback?> GetFeedbackByIdAsync(Guid id);
        Task<List<Feedback>> GetAllFeedbackAsync();
        Task<Feedback> CreateFeedbackAsync(Feedback feedback);
        Task<Feedback?> UpdateFeedbackAsync(Feedback feedback);
        Task<Feedback?> DeleteFeedbackById(Guid id);

    }
}
