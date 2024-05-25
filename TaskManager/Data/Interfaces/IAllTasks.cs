using MyTask = TaskManager.Data.Models.MyTask;
namespace TaskManager.Data.Interfaces
{
    public interface IAllTasks
    {
        IEnumerable<MyTask> Tasks { get; }
        IEnumerable<MyTask> getCompletedTasks { get; }
        IEnumerable<MyTask> getUnCompletedTasks { get; }

        MyTask GetObjectTask(int id);
        void AddTask(MyTask newTask);
        void UpdateTask(MyTask updatedTask);
        void DeleteTask(int id);
    }
}
