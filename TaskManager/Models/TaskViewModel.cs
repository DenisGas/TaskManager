using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManager.Data.Models;

namespace TaskManager.ViewModels
{
    public class TaskViewModel
    {
        public MyTask MyTask { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<MyTask> Tasks { get; set; }
        public string CurrentFilter { get; set; }
    }
}
