using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using MyPortfolioWebApp.Models;
using MyPortfolioWebApp.Models.DatabaseModels;
using MyPortfolioWebApp.Models.ViewModels;

namespace MyPortfolioWebApp.Controllers
{
    public class AboutMeController : Controller
    {
        // GET: AboutMe
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            AboutMe aboutMe = (from u in db.AboutMe
                               select u).FirstOrDefault();

            AboutMeViewModel aboutMeViewModel = Mapper.Map<AboutMe, AboutMeViewModel>(aboutMe);
            return View(aboutMeViewModel);
        }
    }
}