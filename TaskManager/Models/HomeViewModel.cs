using TaskManager.Data.Models;
using System.Collections.Generic;

namespace TaskManager.Models
{
    public class HomeViewModel
    {
        public IEnumerable<MyTask> TasksForToday { get; set; }
        public IEnumerable<MyTask> Last10Tasks { get; set; }
    }
}
