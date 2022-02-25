using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [StringLength(25)]
        public string TaskDescription { get; set; } // Add Validation Tag Helper in View

        public enum PriorityLevel { Low, Normal, High }
        public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

        public bool IsCompleted { get; set; } = false;
    }
}
