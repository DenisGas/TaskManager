namespace TaskManager.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; } // Изменено, чтобы допускать null

        public ICollection<MyTask> Tasks { get; set; } // Навигационное свойство для задач в этой категории
    }
}
