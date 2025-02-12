using Product_Feedback_App.Models.Identity;

namespace Product_Feedback_App.Models.Domain
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public AppUser User { get; set; }

        public string Content { get; set; }

        public string TargetUsername { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Comment? ParentComment { get; set; }
    }
}
