using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Data.Repos
{
    public class TaskRepos : IAllTasks
    {
        private readonly AppDbContent appDbContent;

        public TaskRepos(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IEnumerable<MyTask> Tasks => appDbContent.Tasks.Include(t => t.Categories);
        public IEnumerable<MyTask> getCompletedTasks => appDbContent.Tasks.Where(t => t.IsCompleted).Include(t => t.Categories);
        public IEnumerable<MyTask> getUnCompletedTasks => appDbContent.Tasks.Where(t => !t.IsCompleted).Include(t => t.Categories);

        public MyTask GetObjectTask(int id)
        {
            return appDbContent.Tasks.Include(t => t.Categories).FirstOrDefault(t => t.Id == id);
        }

        public void AddTask(MyTask newTask)
        {
            if (newTask.Categories != null && newTask.Categories.Count > 0)
            {
                foreach (var category in newTask.Categories)
                {
                    appDbContent.Categories.Attach(category);
                }
            }

            appDbContent.Tasks.Add(newTask);
            appDbContent.SaveChanges();
        }

        public void UpdateTask(MyTask updatedTask)
        {
            var task = appDbContent.Tasks
                .Include(t => t.Categories)
                .FirstOrDefault(t => t.Id == updatedTask.Id);

            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.UpdatedDate = DateTime.Now; // Оновлюємо дату оновлення
                task.DueDate = updatedTask.DueDate;
                task.IsCompleted = updatedTask.IsCompleted;
                task.Priority = updatedTask.Priority;

                // Опціонально: оновлюємо категорії, якщо вони змінилися
                if (!Enumerable.SequenceEqual(task.Categories, updatedTask.Categories))
                {
                    task.Categories.Clear(); // Очищаємо наявні категорії
                    foreach (var category in updatedTask.Categories)
                    {
                        appDbContent.Categories.Attach(category);
                        task.Categories.Add(category); // Додаємо нові категорії
                    }
                }

                appDbContent.Tasks.Update(task);
                appDbContent.SaveChanges();
            }
        }


        public void DeleteTask(int id)
        {
            var task = appDbContent.Tasks.Include(t => t.Categories).FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                appDbContent.Tasks.Remove(task);
                appDbContent.SaveChanges();
            }
        }
    }


}
