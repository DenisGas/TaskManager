using MyTask = TaskManager.Data.Models.MyTask;
namespace TaskManager.Data.Interfaces
{
    public interface IAllTasks
    {
        IEnumerable<MyTask> Tasks { get; set; }
        IEnumerable<MyTask> getCompletedTasks { get; set; }
        IEnumerable<MyTask> getUnCompletedTasks { get; set; }

        MyTask GetObjectTask(int id);

    }
}
