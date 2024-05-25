using TaskManager.Data.Models;
using System.Collections.Generic;

namespace TaskManager.Data.Interfaces
{
    public interface ITasksCategory
    {
        IEnumerable<Category> AllCategories { get; }
        Category GetCategoryById(int categoryId); // Метод для получения категории по Id
        void AddCategory(Category category); // Метод для добавления новой категории
        void DeleteCategory(int categoryId); // Метод для удаления категории по Id
    }
}
