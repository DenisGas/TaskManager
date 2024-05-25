using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Data.Models;

namespace TaskManager.Data
{
    public class DbObject
    {
        public static void Initial(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDbContent appDbContent = scope.ServiceProvider.GetRequiredService<AppDbContent>();

                if (!appDbContent.Categories.Any())
                {
                    // Create and add categories to the database if they do not exist
                    var categories = new List<Category>
                    {
                        new Category { CategoryName = "Programming", Description = "All tasks related to programming" },
                        new Category { CategoryName = "Cooking", Description = "All tasks related to cooking" },
                        new Category { CategoryName = "Shopping", Description = "All tasks related to shopping" }
                    };

                    appDbContent.Categories.AddRange(categories);
                    appDbContent.SaveChanges();
                }

                if (!appDbContent.Tasks.Any())
                {
                    // Create and add tasks to the database if they do not exist
                    var initialTasks = new List<MyTask>
                    {
                        new MyTask { Title = "Complete project", Description = "Finish the development of the project", DueDate = DateTime.Now.AddDays(7), Priority = PriorityLevel.High, Categories = appDbContent.Categories.Where(c => c.CategoryName == "Programming").ToList() },
                        new MyTask { Title = "Grocery shopping", Description = "Buy groceries for the week", DueDate = DateTime.Now.AddDays(2), Priority = PriorityLevel.Medium, Categories = appDbContent.Categories.Where(c => c.CategoryName == "Shopping").ToList() },
                        new MyTask { Title = "Read book", Description = "Read the new book by favorite author", DueDate = DateTime.Now.AddDays(14), Priority = PriorityLevel.Low, Categories = appDbContent.Categories.Where(c => c.CategoryName == "Reading").ToList() }
                    };

                    // Add the created tasks to the database
                    appDbContent.Tasks.AddRange(initialTasks);
                    appDbContent.SaveChanges();

                    // Initialize and populate the Tasks dictionary
                    foreach (var task in initialTasks)
                    {
                        Tasks.Add(task.Title, task);
                    }
                }
            }
        }

        private static Dictionary<string, MyTask> tasks;

        public static Dictionary<string, MyTask> Tasks
        {
            get
            {
                if (tasks == null)
                {
                    tasks = new Dictionary<string, MyTask>();
                }
                return tasks;
            }
        }

        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    category = new Dictionary<string, Category>();
                }
                return category;
            }
        }
    }
}
