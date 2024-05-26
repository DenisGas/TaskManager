using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data.Models
{
    public class MyTask
    {
        public MyTask()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            Priority = PriorityLevel.Low;
            Categories = new List<Category>();
            DueDate = DateTime.Now.AddDays(1);
            IsCompleted = false;
            CategoryIds = new List<int>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public List<int>? CategoryIds { get; set; }

        public List<Category> Categories { get; set; }

        public PriorityLevel Priority { get; set; }
    }



    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
