using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Models;

namespace TaskManager.Data
{
    public class AppDbContent : DbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options)
        {
        }

        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize the many to many relationship between tasks and categories
            modelBuilder.Entity<MyTask>()
                .HasMany(t => t.Categories)
                .WithMany(c => c.Tasks)
                .UsingEntity(j => j.ToTable("TaskCategories"));
        }
    }
}
