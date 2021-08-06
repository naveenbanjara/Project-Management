using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.Models
{
    public class Attachments
    {
        [Key]
        public int IssueID { get; set; }
        public string Filename { get; set; }        
        public Issue Issue { get; set; }
        
    }
}
