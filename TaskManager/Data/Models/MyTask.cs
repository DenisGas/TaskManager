namespace TaskManager.Data.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } // Час, коли завдання має бути завершено
        public bool IsCompleted { get; set; } // Статус завдання: виконане чи ні

        public int CategoryId { get; set; } // Зовнішній ключ для категорії
        public Category Category { get; set; } // Навігаційна властивість для категорії

        public PriorityLevel Priority { get; set; } // Пріоритет завдання
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
