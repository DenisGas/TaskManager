using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Data.Mocks
{
    public class MockCategory : ITasksCategory
    {
        private readonly List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Programming", Description = "All tasks related to programming" },
            new Category { Id = 2, CategoryName = "Cooking", Description = "All tasks related to cooking" },
            new Category { Id = 3, CategoryName = "Shopping", Description = "All tasks related to shopping" }
        };

        public IEnumerable<Category> AllCategories => _categories;

        public Category GetCategoryById(int categoryId) => _categories.FirstOrDefault(c => c.Id == categoryId);

        public void AddCategory(Category category)
        {
            category.Id = _categories.Max(c => c.Id) + 1; // Генерация нового Id
            _categories.Add(category);
        }
    }
}
