using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.Models
{
    public class Issue
    {
        public enum status
        {
            open,
            closed,
            reopened
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public Project Project { get; set; }
        public int ProjectID { get; set; }
        public ICollection<Attachments> Attachments { get; set; }
        [NotMapped]
        public List<IFormFile> FormFiles { get; set; }
        public ICollection<FileUpload> FileUploads { get; set; }
    }
}
