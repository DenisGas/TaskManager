using TaskManager.Data.Interfaces;
using TaskManager.Data.Mocks;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Repos;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IAllTasks, TaskRepos>();
            builder.Services.AddScoped<ITasksCategory, CategoryRepos>();
            builder.Services.AddDbContext<AppDbContent>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            DbObject.Initial(app);

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
