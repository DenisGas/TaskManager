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
            // ѕолучаем все задани€
            var allTasks = _allTasks.Tasks;

            // ѕолучаем задани€ на сегодн€
            var tasksForToday = allTasks.Where(task => task.DueDate.Date == DateTime.Today);

            // ≈сли заданий на сегодн€ нет, создаем пустой список
            if (!tasksForToday.Any())
            {
                tasksForToday = new List<MyTask>();
            }

            // ѕолучаем последние 10 заданий
            var last10Tasks = allTasks.OrderByDescending(task => task.CreatedDate).Take(10);

            // —оздаем модель представлени€, котора€ содержит как задани€ на сегодн€, так и последние 10 заданий
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
