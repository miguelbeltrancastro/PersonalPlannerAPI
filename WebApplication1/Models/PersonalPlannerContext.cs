using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class PersonalPlannerContext : DbContext
    {
        public PersonalPlannerContext(DbContextOptions<PersonalPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<FocusTask> FocusTask { get; set; } = null!;
    }
}