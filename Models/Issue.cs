using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.Models
{
    public class Issue
    {
        public enum status
        {
            open,
            closed
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? attachment { get; set; }
        public Project Project { get; set; }
        public int ProjectID { get; set; }
    }
}
