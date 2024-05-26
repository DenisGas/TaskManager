using System;
using System.Collections.Generic;
using TaskManager.Data.Models;

namespace TaskManager.ViewModels
{
    public class TaskEditViewModel
    {

        public MyTask Task { get; set; }
        public List<Category>? Categories { get; set; }

    }
}
