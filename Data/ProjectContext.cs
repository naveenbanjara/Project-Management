using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Management.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Attachments> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Issue>().ToTable("Issue");
            modelBuilder.Entity<Attachments>().ToTable("Attachment");
        }
    }
}
