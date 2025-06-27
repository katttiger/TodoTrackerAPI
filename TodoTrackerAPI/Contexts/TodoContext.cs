using Microsoft.EntityFrameworkCore;
using TodoTrackerAPI.Models;

namespace TodoTrackerAPI.Contexts
{
    public class TodoContext:DbContext
    {
        public DbSet<TodoItem> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=Todo; Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
