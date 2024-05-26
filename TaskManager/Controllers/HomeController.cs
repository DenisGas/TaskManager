using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Interfaces;
using TaskManager.Data.Models;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAllTasks _allTasks;

        public HomeController(IAllTasks allTasks, ILogger<HomeController> logger)
        {
            _allTasks = allTasks;

            _logger = logger;

        }
        public IActionResult Index()
        {
            // Отримуємо всі завдання
            var allTasks = _allTasks.Tasks;

            // Отримуємо завдання на сьогодні
            var tasksForToday = allTasks.Where(task => task.DueDate.Date == DateTime.Today && !task.IsCompleted)
                                        .OrderBy(task => task.Priority)
                                        .ToList();

            // Если задач на сегодня нет, создаем пустой список
            if (!tasksForToday.Any())
            {
                tasksForToday = new List<MyTask>();
            }

            // Отримуємо останні 10 завдань
            var last10Tasks = allTasks.Where(task => !task.IsCompleted).OrderByDescending(task => task.CreatedDate).Take(10);

            // Створюємо модель подання, яка містить як завдання на сьогодні, так і останні 10 завдань
            var viewModel = new HomeViewModel
            {
                TasksForToday = tasksForToday,
                Last10Tasks = last10Tasks
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


