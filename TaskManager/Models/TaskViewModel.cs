using System;
using System.Collections.Generic;
using TaskManager.Data.Models;

namespace TaskManager.ViewModels
{
    public class TaskViewModel
    {
        public IEnumerable<MyTask> AllTasks { get; set; }
        public string CurrentFilter { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<PriorityLevel> PriorityLevels { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<Category> AllCategories { get; set; }
        public bool? IsCompletedFilter { get; internal set; }
    }

}
