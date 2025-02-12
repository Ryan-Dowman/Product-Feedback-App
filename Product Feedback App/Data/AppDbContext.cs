using Microsoft.EntityFrameworkCore;
using Product_Feedback_App.Models.Domain;

namespace Product_Feedback_App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
