using TaskManager.Data.Models;
using System.Collections.Generic;

namespace TaskManager.Data.Interfaces
{
    public interface ITasksCategory
    {
        IEnumerable<Category> AllCategories { get; }
        Category GetCategoryById(int categoryId); 
        void AddCategory(Category category); 
        void DeleteCategory(int categoryId); 
    }
}
