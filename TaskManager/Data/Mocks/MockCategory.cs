using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;

namespace TaskManager.Data.Mocks
{
    public class MockCategory : ITasksCategory
    {
        public IEnumerable<Category> AllCategories {
            get
            {
                return new List<Category>
               {
                   new Category { CategoryName = "Програмування", Description = "Інформація про категорію програмування"},
                   new Category { CategoryName = "Готовка", Description = "Інформація про категорію готовку" }
               };
            }
        
        }
    }
}
