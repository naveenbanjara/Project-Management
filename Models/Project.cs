using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.Models
{
    public class Project
    {
        public enum status
        {
            Design,
            Development,
            Testing,
            Release,
            Update
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? EndDate { get; set; }
        public status Status { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
