using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManager.Data.Models;

namespace TaskManager.ViewModels
{
    public class TaskViewModel
    {
        public IEnumerable<MyTask> AllTasks { get; set; } 
        public string CurrentFilter { get; set; }
    }
}
