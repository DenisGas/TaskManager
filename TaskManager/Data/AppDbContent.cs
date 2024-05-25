using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Models;
namespace TaskManager.Data
{
    public class AppDbContent : DbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options) {
        
        }

        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }


/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=TaskManager;Trusted_Connection=True;ConnectRetryCount=0");
        }*/

    }
}
