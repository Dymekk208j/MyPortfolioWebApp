using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class ProjectListModelView
    {
        public List<ProjectViewModel> List { get; set; }

        public ProjectListModelView()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List = new List<ProjectViewModel>();

            var projectsList = from u in db.Projects
                select u;

            foreach (var project in projectsList)
            {
                var p = Mapper.Map<Project, ProjectViewModel>(project);
                List.Add(p);
            }
        }
    }
}