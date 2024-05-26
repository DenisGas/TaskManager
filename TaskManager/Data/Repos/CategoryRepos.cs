using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Data.Repos
{
    public class CategoryRepos : ITasksCategory
    {
        private readonly AppDbContent appDbContent;

        public CategoryRepos(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IEnumerable<Category> AllCategories => appDbContent.Categories;

        public Category GetCategoryById(int categoryId)
        {
            return appDbContent.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public void AddCategory(Category category)
        {
            if (!appDbContent.Categories.Any(c => c.CategoryName == category.CategoryName))
            {
                appDbContent.Categories.Add(category);
                appDbContent.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var category = appDbContent.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                appDbContent.Categories.Remove(category);
                appDbContent.SaveChanges();
            }
        }
    }
}
