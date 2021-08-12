using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.Models
{
    public class FileUpload
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public int IssueID { get; set; }
        public Issue Issue { get; set; }
    }
}
