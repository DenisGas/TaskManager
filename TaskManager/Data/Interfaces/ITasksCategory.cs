using TaskManager.Data.Models;

namespace TaskManager.Data.Interfaces
{
    public interface ITasksCategory
    {
       IEnumerable<Category> AllCategories { get; }
    }
}
