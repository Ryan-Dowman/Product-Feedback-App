using Product_Feedback_App.Models.Enums;

namespace Product_Feedback_App.Models.Domain
{
    public class Feedback
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public Status Status { get; set; }

        public string Details { get; set; }

        public ICollection<Upvote> Upvotes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
