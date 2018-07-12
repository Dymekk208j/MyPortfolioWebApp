using MyPortfolioWebApp.Models.ViewModels;
using System.Collections.Generic;
using MyPortfolioWebApp.Models.DatabaseModels;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult Index()
        {
            ProjectsListViewModel projectsListViewModel = new ProjectsListViewModel();
            projectsListViewModel.List = new List<ProjectViewModel>();
            ApplicationDbContext db = new ApplicationDbContext();

            var nws = (from z in db.Projects
                       select z).OrderByDescending(x => x.DateTimeCreated);


            foreach (var nw in nws)
            {
                ProjectViewModel projectViewModel = Mapper.Map<Project, ProjectViewModel>(nw);

                if (projectViewModel != null) projectsListViewModel.List.Add(projectViewModel);
            }

            return View(projectsListViewModel);
        }

        public ActionResult ProjectView(int projectId)
        {
            ApplicationDbContext db3 = new ApplicationDbContext();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationDbContext db2 = new ApplicationDbContext();


            var pvm = (from x in db3.Projects
                where x.ProjectId == projectId
                select x).FirstOrDefault();
            ProjectViewModel projectViewModel = Mapper.Map<Project, ProjectViewModel>(pvm);
            projectViewModel.Technologies = new List<TechnologyViewModel>();

            var technologiesList = from u in db.ProjectTechnologies
                where u.ProjectId == pvm.ProjectId
                select u;

            foreach (var porojectTechnology in technologiesList)
            {
                var technology = (from z in db2.Technologies
                    where z.TechnologyId == porojectTechnology.TechnologyId
                    select z).FirstOrDefault();

                if (technology == null) continue;
                var tech = Mapper.Map<Technology, TechnologyViewModel>(technology);

                projectViewModel.SelectedTechnology = null;
                projectViewModel.Technologies.Add(tech);
            }

            return View("ProjectView", projectViewModel);
        }

    }

}