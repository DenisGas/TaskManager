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
        private int _nextTaskId;

        public TasksController(IAllTasks allTasks, ITasksCategory allCategories)
        {
            _allTasks = allTasks;
            _allCategories = allCategories;
            _nextTaskId = _allTasks.Tasks.Any() ? _allTasks.Tasks.Max(t => t.Id) + 1 : 1;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        public IActionResult List(string searchString)
        {
            // Зберігаємо рядок запиту в ViewBag
            ViewBag.CurrentFilter = searchString;

            var tasks = _allTasks.Tasks;

           
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return View(tasks);
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
                // Увеличиваем значение _nextTaskId
                _nextTaskId++;

                // Присваиваем новый Id задаче
                myTask.Id = _nextTaskId;

                // Добавляем новую задачу к списку задач
                _allTasks.Tasks = _allTasks.Tasks.Append(myTask).ToList();

                // Перенаправляем на страницу с данными новой задачи
                return RedirectToAction(nameof(Details), new { id = myTask.Id });
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
