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
                    new() {    Title = "Complete report",
                    Description = "Complete the financial report for Q1",
                    CreatedDate = DateTime.Now,
                    IsCompleted = false,
                    DueDate = DateTime.Now.AddDays(2),
                    Priority = PriorityLevel.High,
                    Category = _tasksCategory.AllCategories.First()},
                    new() {    Title = "Write report",
                    Description = "Complete the Tech report for Q2",
                    CreatedDate = DateTime.Now,
                    IsCompleted = true,
                    DueDate = DateTime.Now.AddDays(5),
                    Priority = PriorityLevel.Medium,
                    Category = _tasksCategory.AllCategories.Last()},
                    new() {    Title = "Купить Корм",
                    Description = "Complete the financial report for Q2",
                    CreatedDate = DateTime.Now,
                    IsCompleted = true,
                    DueDate = DateTime.Now.AddDays(0),
                    Priority = PriorityLevel.Low,
                    Category = _tasksCategory.AllCategories.Last()}
                }; set => throw new NotImplementedException();
        }
        public IEnumerable<MyTask> getCompletedTasks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<MyTask> getUnCompletedTasks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MyTask GetObjectTask(int id)
        {
            throw new NotImplementedException();
        }
    }
}
