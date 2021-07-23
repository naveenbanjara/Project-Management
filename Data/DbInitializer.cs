using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Management.Models;

namespace Project_Management.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProjectContext context)
        {
            context.Database.EnsureCreated();

            if (context.Projects.Any())
            {
                return;
            }

            var projects = new Project[]
            {
                new Project{Name="Project 1",Description="Decription",StartingDate=DateTime.Parse("2005-09-01"),Deadline=DateTime.Parse("2005-11-01"),EndDate=DateTime.Parse("2005-10-28"),Status=Project.status.Update},
                new Project{Name="Project 2",Description="Decription",StartingDate=DateTime.Parse("2005-12-10"),Deadline=DateTime.Parse("2006-11-14"),EndDate=DateTime.Parse("2006-09-12"),Status=Project.status.Release},
                new Project{Name="Project 3",Description="Decription",StartingDate=DateTime.Parse("2006-01-01"),Deadline=DateTime.Parse("2006-04-24"),Status=Project.status.Development},
                new Project{Name="Project 4",Description="Decription",StartingDate=DateTime.Parse("2006-03-11"),Deadline=DateTime.Parse("2006-08-07"),EndDate=DateTime.Parse("2006-09-20"),Status=Project.status.Update},
                new Project{Name="Project 5",Description="Decription",StartingDate=DateTime.Parse("2006-05-31"),Deadline=DateTime.Parse("2006-10-05"),Status=Project.status.Testing},
                new Project{Name="Project 6",Description="Decription",StartingDate=DateTime.Parse("2006-05-31"),Deadline=DateTime.Parse("2007-01-17"),Status=Project.status.Design}
            };

            foreach (Project project in projects)
            {
                context.Projects.Add(project);
            }
            context.SaveChanges();

            var issues = new Issue[]
            {
                new Issue{Title="Issue 1",Description="Decription",Status=Issue.status.open,StartDate=DateTime.Parse("2005-12-12"),ProjectID=1},
                new Issue{Title="Issue 2",Description="Decription",Status=Issue.status.open,StartDate=DateTime.Parse("2006-12-12"),ProjectID=2},
                new Issue{Title="Issue 3",Description="Decription",Status=Issue.status.closed,StartDate=DateTime.Parse("2005-12-12"),EndDate=DateTime.Parse("2006-03-23"),ProjectID=1},
                new Issue{Title="Issue 4",Description="Decription",Status=Issue.status.open,StartDate=DateTime.Parse("2006-12-12"),ProjectID=3},
                new Issue{Title="Issue 5",Description="Decription",Status=Issue.status.open,StartDate=DateTime.Parse("2006-12-12"),ProjectID=4},
                new Issue{Title="Issue 6",Description="Decription",Status=Issue.status.closed,StartDate=DateTime.Parse("2006-12-12"),EndDate=DateTime.Parse("2006-02-28"),ProjectID=3},
                new Issue{Title="Issue 7",Description="Decription",Status=Issue.status.open,StartDate=DateTime.Parse("2006-12-12"),ProjectID=5},
                new Issue{Title="Issue 8",Description="Decription",Status=Issue.status.closed,StartDate=DateTime.Parse("2006-12-12"),EndDate=DateTime.Parse("2006-04-14"),ProjectID=2}
            };
            foreach (Issue issue in issues)
            {
                context.Issues.Add(issue);
            }
            context.SaveChanges();
        }
    }
}
