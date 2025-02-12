namespace Product_Feedback_App.Models.Domain
{
    public class Upvote
    {
        public Guid Id { get; set; }

        public Guid FeedbackId { get; set; }

        public Guid UserId { get; set; }
    }
}
