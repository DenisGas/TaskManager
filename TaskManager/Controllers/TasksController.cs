    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Data.Interfaces;
    using TaskManager.Data.Models;
    using TaskManager.ViewModels;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
using TaskManager.Data;

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

            // Применение фильтров
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

            // Применение фильтра по статусу
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




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, MyTask updatedTask, string newCategoryName)
        {
            // Виводимо дані для перевірки у консоль
            Console.WriteLine("Updated Task Title: " + updatedTask.Title);
            Console.WriteLine("Updated Task Description: " + updatedTask.Description);
            Console.WriteLine("Updated Due Date: " + updatedTask.DueDate);
            Console.WriteLine("Updated Priority: " + updatedTask.Priority);
            Console.WriteLine("Updated Selected Categories: " + (updatedTask.CategoryIds != null ? string.Join(", ", updatedTask.CategoryIds) : "None"));
            Console.WriteLine("New Category Name: " + newCategoryName);

            if (ModelState.IsValid)
            {
                // Додаємо нові категорії, якщо вони вказані
                if (!string.IsNullOrEmpty(newCategoryName))
                {
                    var newCategoryNames = newCategoryName.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var name in newCategoryNames)
                    {
                        var trimmedName = name.Trim();
                        if (!string.IsNullOrEmpty(trimmedName))
                        {
                            var newCategory = new Category { CategoryName = trimmedName, Description = string.Empty };
                            _allCategories.AddCategory(newCategory);

                            // Додаємо ID нової категорії до завдання
                            if (updatedTask.CategoryIds == null)
                            {
                                updatedTask.CategoryIds = new List<int>();
                            }
                            updatedTask.CategoryIds.Add(newCategory.Id);
                        }
                    }
                }

                // Отримуємо категорії за ID та встановлюємо їх для завдання
                if (updatedTask.CategoryIds != null && updatedTask.CategoryIds.Count > 0)
                {
                    updatedTask.Categories = _allCategories.AllCategories
                        .Where(c => updatedTask.CategoryIds.Contains(c.Id))
                        .ToList();
                }

                // Викликаємо метод UpdateTask для оновлення завдання
                _allTasks.UpdateTask(updatedTask);

                // Перенаправляємо на сторінку з деталями оновленого завдання
                return RedirectToAction("Details", new { id = updatedTask.Id });
            }

            // Якщо модель недійсна, передаємо список категорій у представлення разом із зміненим завданням
            var categories = _allCategories.AllCategories.ToList();
            updatedTask.Categories = categories;

            return View(updatedTask);
        }




        [HttpPost]
        public IActionResult UpdateStatus(int id, bool isCompleted, string returnUrl)
        {
            // Найдем задачу по ID
            var task = _allTasks.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine($"Завдання з ID {id} не знайдено.");
                return NotFound();
            }

            // Обновим статус задачи
            task.IsCompleted = isCompleted;
            _allTasks.UpdateTask(task);

            Console.WriteLine($"Завдання з ID {id} оновлено. Новий статус: {isCompleted}");

            // Перенаправим на указанный адрес
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

            // Вернем успешный результат, если адрес не указан
            return Ok();
        }




        public IActionResult Create()
            {
                // Получаем список всех категорий
                var categories = _allCategories.AllCategories.ToList();

                // Создаем новый объект задачи и инициализируем список категорий
                var myTask = new MyTask { Categories = categories };

                // Передаем объект задачи с категориями в представление
                return View(myTask);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MyTask myTask, string? newCategoryName = null) {


            if (!ModelState.IsValid)
            {
                // Определение ошибок в модели и их обработка
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                // Вывод ошибок в консоль для проверки (можно заменить на другие действия с обработкой ошибок)
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // Возврат страницы снова вместе с ошибками валидации
                var categories = _allCategories.AllCategories.ToList();
                myTask.Categories = categories;
                return View(myTask);
            }

            // Виводимо дані в консоль для перевірки
            Console.WriteLine("Task Title: " + myTask.Title);
            Console.WriteLine("Task Description: " + myTask.Description);
            Console.WriteLine("Due Date: " + myTask.DueDate);
            Console.WriteLine("Priority: " + myTask.Priority);
            Console.WriteLine("Selected Categories: " + (myTask.CategoryIds != null ? string.Join(", ", myTask.CategoryIds) : "None"));
            Console.WriteLine("New Category Name: " + !string.IsNullOrEmpty(newCategoryName));


            // Якщо вибрано або введено хоча б одну категорію, то додати їх до моделі
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
                            // Перевірка, чи категорія вже існує в базі даних
                            var existingCategory = _allCategories.AllCategories.FirstOrDefault(c => c.CategoryName.Equals(trimmedName, StringComparison.OrdinalIgnoreCase));
                            if (existingCategory != null)
                            {
                                // Додаємо ID існуючої категорії до завдання
                                if (myTask.CategoryIds == null)
                                {
                                    myTask.CategoryIds = new List<int>();
                                }
                                myTask.CategoryIds.Add(existingCategory.Id);
                            }
                            else
                            {
                                // Якщо категорія нова, то створюємо її та додаємо ID до завдання
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

                // Отримуємо категорії за ID та встановлюємо їх для завдання
                if (myTask.CategoryIds != null && myTask.CategoryIds.Count > 0)
                {
                    myTask.Categories = _allCategories.AllCategories
                        .Where(c => myTask.CategoryIds.Contains(c.Id))
                        .ToList();
                }

                // Викликаємо метод AddTask для додавання нового завдання
                _allTasks.AddTask(myTask);

                // Перенаправляємо на сторінку з деталями нового завдання
                return RedirectToAction("List");
            }

            // Якщо категорії не вибрані і не введені, додаємо пустий список категорій до завдання
            myTask.Categories = new List<Category>();

            // Викликаємо метод AddTask для додавання нового завдання
            _allTasks.AddTask(myTask);


            // Перенаправляємо на сторінку з деталями нового завдання
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
