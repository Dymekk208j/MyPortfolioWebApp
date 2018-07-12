using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using MyPortfolioWebApp.Models;
using MyPortfolioWebApp.Models.DatabaseModels;
using MyPortfolioWebApp.Models.ViewModels;

namespace MyPortfolioWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(GetLastProject());
        }
  
        public ProjectViewModel GetLastProject()
        {
            ProjectViewModel project = new ProjectViewModel();
            ApplicationDbContext db = new ApplicationDbContext();


            var nws = from z in db.Projects select z;


            nws = nws.OrderByDescending(d => d.DateTimeCreated);
            nws = nws.Take(1);

            foreach (var nw in nws)
            {
                project = Mapper.Map<Project, ProjectViewModel>(nw);
            }


            return project;
        }
    }
}