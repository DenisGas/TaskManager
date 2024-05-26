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
            // �������� �� ��������
            var allTasks = _allTasks.Tasks;

            // �������� �������� �� �������
            var tasksForToday = allTasks.Where(task => task.DueDate.Date == DateTime.Today && !task.IsCompleted)
                                        .OrderBy(task => task.Priority)
                                        .ToList();

            // ���� ����� �� ������� ���, ������� ������ ������
            if (!tasksForToday.Any())
            {
                tasksForToday = new List<MyTask>();
            }

            // �������� ������ 10 �������
            var last10Tasks = allTasks.Where(task => !task.IsCompleted).OrderByDescending(task => task.CreatedDate).Take(10);

            // ��������� ������ �������, ��� ������ �� �������� �� �������, ��� � ������ 10 �������
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


