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
        public IEnumerable<MyTask> getCompletedTasks => appDbContent.Tasks.Where(t => t.IsCompleted);
        public IEnumerable<MyTask> getUnCompletedTasks => appDbContent.Tasks.Where(t => t.IsCompleted == false);

        public MyTask GetObjectTask(int id)
        {
            return appDbContent.Tasks.FirstOrDefault(t => t.Id == id);
        }

        // Метод для добавления новой задачи
        public void AddTask(MyTask newTask)
        {
            appDbContent.Tasks.Add(newTask);
            appDbContent.SaveChanges();
        }

        // Метод для обновления существующей задачи
        public void UpdateTask(MyTask updatedTask)
        {
            var task = appDbContent.Tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.CreatedDate = updatedTask.CreatedDate;
                task.UpdatedDate = updatedTask.UpdatedDate;
                task.DueDate = updatedTask.DueDate;
                task.IsCompleted = updatedTask.IsCompleted;
                task.Priority = updatedTask.Priority;
                task.Categories = updatedTask.Categories;

                appDbContent.Tasks.Update(task);
                appDbContent.SaveChanges();
            }
        }

        // Метод для удаления задачи
        public void DeleteTask(int id)
        {
            var task = appDbContent.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                appDbContent.Tasks.Remove(task);
                appDbContent.SaveChanges();
            }
        }
    }
}
