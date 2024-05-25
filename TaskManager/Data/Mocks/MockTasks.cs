using System.Collections.Generic;
using System.Linq;
using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;
using MyTask = TaskManager.Data.Models.MyTask;

namespace TaskManager.Data.Mocks
{
    public class MockTasks : IAllTasks
    {
        private readonly ITasksCategory _tasksCategory = new MockCategory();


        public IEnumerable<MyTask> Tasks
        {
            get => new List<MyTask>
                {
                    new()
                    {
                        Id = 0,
                        Title = "Complete report",
                        Description = "Complete the financial report for Q1",
                        CreatedDate = DateTime.Now,
                        IsCompleted = false,
                        DueDate = DateTime.Now.AddDays(2),
                        Priority = PriorityLevel.High,
                        Categories = new List<Category>
                        {
                            _tasksCategory.GetCategoryById(1), // Programming
                            _tasksCategory.GetCategoryById(2)  // Cooking
                        }
                    },
                    new()
                    {
                        Id = 1,
                        Title = "Write report",
                        Description = "Complete the Tech report for Q2",
                        CreatedDate = DateTime.Now,
                        IsCompleted = true,
                        DueDate = DateTime.Now.AddDays(5),
                        Priority = PriorityLevel.Medium,
                        Categories = new List<Category>
                        {
                            _tasksCategory.GetCategoryById(2)  // Cooking
                        }
                    },
                    new()
                    {
                        Id = 2,
                        Title = "Купить Корм",
                        Description = "Ням Ням",
                        CreatedDate = DateTime.Now,
                        IsCompleted = true,
                        DueDate = DateTime.Now.AddDays(0),
                        Priority = PriorityLevel.Low,
                        Categories = new List<Category>
                        {
                            _tasksCategory.GetCategoryById(3)  // Shopping
                        }
                    },
                    new()
                    {
                        Id = 3,
                        Title = "Купить Кота",
                        Description = "Мяу",
                        CreatedDate = DateTime.Now,
                        IsCompleted = false,
                        DueDate = DateTime.Now.AddDays(0),
                        Priority = PriorityLevel.Low,
                        Categories = new List<Category>
                        {
                            _tasksCategory.GetCategoryById(3)  // Shopping
                        }
                    }
                };
            set => throw new NotImplementedException();
        }

        public IEnumerable<MyTask> getCompletedTasks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<MyTask> getUnCompletedTasks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddTask(MyTask newTask)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(int id)
        {
            throw new NotImplementedException();
        }

        public MyTask GetObjectTask(int id)
        {
            return Tasks.FirstOrDefault(task => task.Id == id);
        }

        public void UpdateTask(MyTask updatedTask)
        {
            throw new NotImplementedException();
        }
    }
}
