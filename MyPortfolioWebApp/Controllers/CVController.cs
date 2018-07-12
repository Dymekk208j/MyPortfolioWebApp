using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyPortfolioWebApp.Models;
using MyPortfolioWebApp.Models.DatabaseModels;
using MyPortfolioWebApp.Models.ViewModels;


namespace MyPortfolioWebApp.Controllers
{
    public class CvController : Controller
    {
        private ApplicationDbContext _db;
        private CvViewModel _cvViewModel;
        public ActionResult Index()
        {
            _cvViewModel = new CvViewModel();
            _db = new ApplicationDbContext();

            _cvViewModel.PrivateInformationViewModel = new PrivateInformationViewModel();

            GetAchivments();
            GetAdditionalInfos();
            GetEducations();
            GetEmploymentHistories();
            GetProjects();
            GetTechnologies();

            return View(_cvViewModel);
        }

        private void GetAchivments()
        {
            _cvViewModel.Achivments = new List<Achivment>();
            var cvm = from u in _db.Achivments
                      select u;

            foreach (Achivment a in cvm)
            {
                _cvViewModel.Achivments.Add(a);
            }
        }

        private void GetAdditionalInfos()
        {
            _cvViewModel.AdditionalInfos = new List<AdditionalInfo>();

            var cvm = from u in _db.AdditionalInfos
                      select u;

            foreach (AdditionalInfo a in cvm)
            {
                _cvViewModel.AdditionalInfos.Add(a);
            }
        }

        private void GetEducations()
        {
            _cvViewModel.Educations = new List<Education>();

            var cvm = from u in _db.Educations
                      select u;

            foreach (Education a in cvm)
            {
                _cvViewModel.Educations.Add(a);
            }
        }

        private void GetEmploymentHistories()
        {
            _cvViewModel.EmploymentHistories = new List<EmploymentHistory>();

            var cvm = from u in _db.EmploymentHistories
                      select u;

            foreach (EmploymentHistory a in cvm)
            {
                _cvViewModel.EmploymentHistories.Add(a);
            }
        }

        private void GetProjects()
        {
            _cvViewModel.Projects = new List<Project>();

            var cvm = from u in _db.Projects
                      select u;

            foreach (Project a in cvm)
            {
                _cvViewModel.Projects.Add(a);
            }
        }
             

        private void GetTechnologies()
        {
            _cvViewModel.Technologies = new List<Technology>();

            var cvm = from u in _db.Technologies
                      select u;

            foreach (Technology a in cvm)
            {
                _cvViewModel.Technologies.Add(a);
            }
        }


    }
}