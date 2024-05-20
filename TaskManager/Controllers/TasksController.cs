using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Interfaces;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly IAllTasks _allTasks;
        private readonly ITasksCategory _AllCategories;


        public TasksController(IAllTasks allTasks, ITasksCategory allCategories)
        {
            _allTasks = allTasks;
            _AllCategories = allCategories;
        }   

        public IActionResult List()
        {
            var tasks = _allTasks.Tasks;
            return View(tasks);
        }
    }
}
