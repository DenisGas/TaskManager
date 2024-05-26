using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;
using TaskManager.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly IAllTasks _allTasks;
        private readonly ITasksCategory _allCategories;

        public TasksController(IAllTasks allTasks, ITasksCategory allCategories)
        {
            _allTasks = allTasks;
            _allCategories = allCategories;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        public IActionResult List(string searchString, List<int> categoryIds, List<PriorityLevel> priorityLevels, DateTime? startDate, DateTime? endDate, bool? isCompletedFilter)
        {
            var tasks = _allTasks.Tasks;

            // Application of filters
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (categoryIds != null && categoryIds.Any())
            {
                tasks = tasks.Where(t => t.Categories.Any(c => categoryIds.Contains(c.Id)));
            }

            if (priorityLevels != null && priorityLevels.Any())
            {
                tasks = tasks.Where(t => priorityLevels.Contains(t.Priority));
            }

            if (startDate.HasValue)
            {
                tasks = tasks.Where(t => t.DueDate.Date >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                tasks = tasks.Where(t => t.DueDate.Date <= endDate.Value.Date);
            }

            // Applying filter by status
            if (isCompletedFilter.HasValue)
            {
                tasks = tasks.Where(t => t.IsCompleted == isCompletedFilter.Value);
            }

            var viewModel = new TaskViewModel
            {
                AllTasks = tasks.ToList(),
                CurrentFilter = searchString,
                CategoryIds = categoryIds,
                PriorityLevels = priorityLevels,
                StartDate = startDate,
                EndDate = endDate,
                IsCompletedFilter = isCompletedFilter,
                AllCategories = _allCategories.AllCategories
            };

            return View(viewModel);
        }


        public IActionResult Delete(int id)
        {
            var task = _allTasks.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _allTasks.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _allTasks.DeleteTask(id);

            return RedirectToAction("List");
        }



        public IActionResult Edit(int id)
        {
            var task = _allTasks.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            var categories = _allCategories.AllCategories.ToList();

            task.CategoryIds = task.Categories.Select(c => c.Id).ToList();

            var viewModel = new TaskEditViewModel
            {
                Task = task,
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskEditViewModel viewModel, string? newCategoryName = null)
        {
            var myTask = viewModel.Task;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                var categories = _allCategories.AllCategories.ToList();
                viewModel.Categories = categories;
                return View(viewModel);
            }

            if (!string.IsNullOrEmpty(newCategoryName) || (myTask.CategoryIds != null && myTask.CategoryIds.Count > 0))
            {
                if (!string.IsNullOrEmpty(newCategoryName))
                {
                    var newCategoryNames = newCategoryName.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var name in newCategoryNames)
                    {
                        var trimmedName = name.Trim();
                        if (!string.IsNullOrEmpty(trimmedName))
                        {
                            var existingCategory = _allCategories.AllCategories.FirstOrDefault(c => c.CategoryName.Equals(trimmedName, StringComparison.OrdinalIgnoreCase));
                            if (existingCategory != null)
                            {
                                if (myTask.CategoryIds == null)
                                {
                                    myTask.CategoryIds = new List<int>();
                                }
                                myTask.CategoryIds.Add(existingCategory.Id);
                            }
                            else
                            {
                                var newCategory = new Category { CategoryName = trimmedName, Description = string.Empty };
                                _allCategories.AddCategory(newCategory);

                                if (myTask.CategoryIds == null)
                                {
                                    myTask.CategoryIds = new List<int>();
                                }
                                myTask.CategoryIds.Add(newCategory.Id);
                            }
                        }
                    }
                }

                if (myTask.CategoryIds != null && myTask.CategoryIds.Count > 0)
                {
                    myTask.Categories = _allCategories.AllCategories
                        .Where(c => myTask.CategoryIds.Contains(c.Id))
                        .ToList();
                }

                _allTasks.UpdateTask(myTask);
                return RedirectToAction("Details", new { id = myTask.Id });
            }

            myTask.Categories = new List<Category>();
            _allTasks.UpdateTask(myTask);

            return RedirectToAction("List");
        }





        [HttpPost]
        public IActionResult UpdateStatus(int id, bool isCompleted, string returnUrl)
        {
            // Find the task by ID
            var task = _allTasks.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine($"Завдання з ID {id} не знайдено.");
                return NotFound();
            }

            // Update task status
            task.IsCompleted = isCompleted;
            _allTasks.UpdateTask(task);

            Console.WriteLine($"Завдання з ID {id} оновлено. Новий статус: {isCompleted}");

            // Redirect to the specified address
            if (!string.IsNullOrEmpty(returnUrl))
            {
                // Ensure the returnUrl is a valid URL, then redirect
                if (Uri.IsWellFormedUriString(returnUrl, UriKind.RelativeOrAbsolute))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    Console.WriteLine("Invalid returnUrl format.");
                    return BadRequest("Invalid returnUrl format.");
                }
            }

            // Return successful result if no address is specified
            return Ok();
        }




        public IActionResult Create()
        {
            // Get the list of all categories
            var categories = _allCategories.AllCategories.ToList();

            // Create a new task object and initialize the list of categories
            var myTask = new MyTask { Categories = categories };

            // Pass the task object with categories to the view
            return View(myTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MyTask myTask, string? newCategoryName = null)
        {


            if (!ModelState.IsValid)
            {
                // Determination of errors in the model and their processing
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                // Output errors to the console for checking (can be replaced by other actions with error handling)
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // Return the page again along with validation errors
                var categories = _allCategories.AllCategories.ToList();
                myTask.Categories = categories;
                return View(myTask);
            }

            // Outputting data to the console for verification
            Console.WriteLine("Task Title: " + myTask.Title);
            Console.WriteLine("Task Description: " + myTask.Description);
            Console.WriteLine("Due Date: " + myTask.DueDate);
            Console.WriteLine("Priority: " + myTask.Priority);
            Console.WriteLine("Selected Categories: " + (myTask.CategoryIds != null ? string.Join(", ", myTask.CategoryIds) : "None"));
            Console.WriteLine("New Category Name: " + !string.IsNullOrEmpty(newCategoryName));


            // If at least one category is selected or entered, add them to the model
            if (!string.IsNullOrEmpty(newCategoryName) || (myTask.CategoryIds != null && myTask.CategoryIds.Count > 0))
            {
                if (!string.IsNullOrEmpty(newCategoryName))
                {
                    var newCategoryNames = newCategoryName.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var name in newCategoryNames)
                    {
                        var trimmedName = name.Trim();
                        if (!string.IsNullOrEmpty(trimmedName))
                        {
                            // Check if the category already exists in the database
                            var existingCategory = _allCategories.AllCategories.FirstOrDefault(c => c.CategoryName.Equals(trimmedName, StringComparison.OrdinalIgnoreCase));
                            if (existingCategory != null)
                            {
                                // Add the ID of the existing category to the task
                                if (myTask.CategoryIds == null)
                                {
                                    myTask.CategoryIds = new List<int>();
                                }
                                myTask.CategoryIds.Add(existingCategory.Id);
                            }
                            else
                            {
                                // If the category is new, then create it and add the ID to the task
                                var newCategory = new Category { CategoryName = trimmedName, Description = string.Empty };
                                _allCategories.AddCategory(newCategory);

                                if (myTask.CategoryIds == null)
                                {
                                    myTask.CategoryIds = new List<int>();
                                }
                                myTask.CategoryIds.Add(newCategory.Id);
                            }
                        }
                    }
                }

                // If the category is new, then create it and add the ID to the task
                if (myTask.CategoryIds != null && myTask.CategoryIds.Count > 0)
                {
                    myTask.Categories = _allCategories.AllCategories
                        .Where(c => myTask.CategoryIds.Contains(c.Id))
                        .ToList();
                }

                // If the category is new, then create it and add the ID to the task
                _allTasks.AddTask(myTask);

                // Redirect to the page with the details of the new task
                return RedirectToAction("List");
            }

            // If no categories are selected and entered, add an empty list of categories to the task
            myTask.Categories = new List<Category>();

            // Call the AddTask method to add a new task
            _allTasks.AddTask(myTask);


            // Redirect to the page with the details of the new task
            return RedirectToAction("List");
        }






        public IActionResult Details(int id)
        {
            var task = _allTasks.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }



    }
}
