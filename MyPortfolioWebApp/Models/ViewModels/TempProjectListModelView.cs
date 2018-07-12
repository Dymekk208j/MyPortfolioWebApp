using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class TempProjectListModelView
    {
        public List<TempProjectViewModel> List { get; set; }

        public TempProjectListModelView()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List = new List<TempProjectViewModel>();

            var projectsList = from u in db.TempProjects
                select u;

            foreach (var project in projectsList)
            {
                var p = Mapper.Map<TempProject, TempProjectViewModel>(project);
                List.Add(p);
            }
        }
    }
}