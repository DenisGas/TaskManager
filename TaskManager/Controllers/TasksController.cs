using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;
using TaskManager.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult List(string searchString)
        {
            // Ініціалізація TaskViewModel
            TaskViewModel obj = new TaskViewModel
            {
                AllTasks = _allTasks.Tasks, // Передбачаємо, що _allTasks.Tasks повертає IEnumerable<MyTask>
                CurrentFilter = searchString // Встановлюємо поточний фільтр
            };

            // Якщо рядок пошуку не порожній, фільтруємо завдання
            if (!string.IsNullOrEmpty(searchString))
            {
                obj.AllTasks = obj.AllTasks.Where(t => t.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return View(obj);
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
        public IActionResult Create(MyTask myTask)
        {
            if (ModelState.IsValid)
            {
                // Вызываем метод AddTask для добавления новой задачи
                _allTasks.AddTask(myTask);

                // Перенаправляем на страницу с данными новой задачи
                return RedirectToAction("List");
            }

            // Если модель недопустима, повторно передаем список категорий в представление вместе с объектом задачи
            var categories = _allCategories.AllCategories.ToList();
            myTask.Categories = categories;

            return View(myTask);
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
