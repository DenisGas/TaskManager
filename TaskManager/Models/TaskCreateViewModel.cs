using TaskManager.Data.Models;

namespace TaskManager.ViewModels
{
    public class TaskCreateViewModel
    {
        public MyTask MyTask { get; set; }
        public List<Category> Categories { get; set; }
        public string NewCategoryName { get; set; }
    }
}
