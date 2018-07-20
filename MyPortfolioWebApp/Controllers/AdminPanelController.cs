using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortfolioWebApp.Models;
using MyPortfolioWebApp.Models.DatabaseModels;
using MyPortfolioWebApp.Models.ViewModels;

namespace MyPortfolioWebApp.Controllers

{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private ApplicationDbContext _db;
        private CvViewModel _cvViewModel;

        public ActionResult UploadProjectIcon(HttpPostedFileBase file, int projectId)
        {
            string error = "";

            _db = new ApplicationDbContext();
            var project = (from u in _db.Projects where u.ProjectId == projectId select u).First();

            var img = System.Drawing.Image.FromStream(file.InputStream, true, true);
            if (img.Width > 150 || img.Height > 150) error = "Maksymalny rozmiar ikony to 150x150px.";
            if (img.Width < 50 || img.Height < 50) error = "Minimalny rozmiar ikony to 50x50px.";

            file.InputStream.Seek(0, SeekOrigin.Begin);

            if (file.ContentLength > 0 && string.IsNullOrEmpty(error))
            {
                BlobConnector.UploadIcon(file, projectId, true);
                project.IsIcon = true;
                _db.Entry(project).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            else error = "Błąd wgrywania pliku";

            return RedirectToAction("EditProjectView", new { projectId, error });
        }

        public ActionResult RemoveIconFromProject(int projectId)
        {
            _db = new ApplicationDbContext();
            var project = (from u in _db.Projects where u.ProjectId == projectId select u).First();

            BlobConnector.RemoveIcon(projectId, true);
            project.IsIcon = false;
            _db.Entry(project).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditProjectView", new { projectId });
        }

        public ActionResult UploadTempProjectIcon(HttpPostedFileBase file, int tempProjectId)
        {
            string error = "";

            _db = new ApplicationDbContext();
            var project = (from u in _db.TempProjects where u.TempProjectId == tempProjectId select u).First();

            var img = System.Drawing.Image.FromStream(file.InputStream, true, true);
            if (img.Width > 150 || img.Height > 150) error = "Maksymalny rozmiar ikony to 150x150px.";
            if (img.Width < 50 || img.Height < 50) error = "Minimalny rozmiar ikony to 50x50px.";

            file.InputStream.Seek(0, SeekOrigin.Begin);

            if (file.ContentLength > 0 && string.IsNullOrEmpty(error))
            {
                BlobConnector.UploadIcon(file, tempProjectId, false);
                project.IsIcon = true;
                _db.Entry(project).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            else error = "Błąd wgrywania pliku";


            return RedirectToAction("CreateTempProjectView2", new { TempProjectId = tempProjectId, error });
        }

        public ActionResult RemoveIconFromTempProject(int tempProjectId)
        {
            _db = new ApplicationDbContext();
            var project = (from u in _db.TempProjects where u.TempProjectId == tempProjectId select u).First();

            BlobConnector.RemoveIcon(tempProjectId, false);
            project.IsIcon = false;
            _db.Entry(project).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("CreateTempProjectView2", new { TempProjectId = tempProjectId });
        }

        public ActionResult CreateProjectFromTempProject(int projectId)
        {
            _db = new ApplicationDbContext();

            var tempProject = (from x in _db.TempProjects
                               where x.TempProjectId == projectId
                               select x).FirstOrDefault();
            Project project = Mapper.Map<TempProject, Project>(tempProject);

            if (tempProject != null) _db.Projects.Add(project);
            _db.SaveChanges();
     
            BlobConnector.MoveIconFromTempToProject(projectId, project.ProjectId);
 
            ApplicationDbContext db2 = new ApplicationDbContext();
            var tempProjectTechnologies = from x in db2.TempProjectTechnologies
                                          where x.ProjectId == projectId
                                          select x;

            foreach (var tempProjectTechnology in tempProjectTechnologies)
            {
                ProjectTechnology projectTechnology = Mapper.Map<TempProjectTechnology, ProjectTechnology>(tempProjectTechnology);
                if (projectTechnology == null) continue;
                projectTechnology.ProjectId = project.ProjectId;
                db2.ProjectTechnologies.Add(projectTechnology);
            }
            db2.SaveChanges();


            var imgList = from z in _db.Images
                          where z.ProjectId == projectId && z.TempraryProject
                          select z;

            foreach (var v in imgList)
            {
                BlobConnector.MoveImageFromTempToProject(v, project.ProjectId);
                v.FileName = "Project" + project.ProjectId + v.OriginalFileName;
                v.TempraryProject = false;
                v.ProjectId = project.ProjectId;
                _db.Entry(v).State = System.Data.Entity.EntityState.Modified;
            }
            _db.SaveChanges();


            RemoveTempProject(projectId);

            return RedirectToAction("ProjectsListView");
        }

        public ActionResult UpdateProject(ProjectViewModel projectViewModel)          
        {
            _db = new ApplicationDbContext();
            Project project = Mapper.Map<ProjectViewModel, Project>(projectViewModel);
            project.DateTimeCreated = DateTime.Now;
            project.ShowInCv = false;
            project.AuthorId = User.Identity.GetUserId();


            _db.Entry(project).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            if (projectViewModel.SelectedTechnology != null)
            {
                AddTechnologyToProject(projectViewModel.ProjectId, projectViewModel.SelectedTechnology.GetValueOrDefault());
            }

            return RedirectToAction("EditProjectView", new { projectId = projectViewModel.ProjectId });
        }

        public ActionResult ProjectsListView()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProjectListModelView projectListModelView = new ProjectListModelView();
            var list = (from i in db.Projects select i).ToList();
            projectListModelView.List = Mapper.Map<List<Project>, List<ProjectViewModel>>(list);

            return View("ProjectsMgt/ProjectsListView", projectListModelView);
        }

        public ActionResult RemoveProject(int projectId)
        {
            _db = new ApplicationDbContext();

            var project = (from u in _db.Projects
                           where u.ProjectId == projectId
                           select u).FirstOrDefault();
            
            BlobConnector.RemoveIcon(projectId, true);

            _db.Entry(project).State = System.Data.Entity.EntityState.Deleted;

            var projectTechnologies = from u in _db.ProjectTechnologies
                                      where u.ProjectId == projectId
                                      select u;

            foreach (var tpt in projectTechnologies)
            {
                _db.Entry(tpt).State = System.Data.Entity.EntityState.Deleted;
            }

            var images = from u in _db.Images
                         where u.ProjectId == projectId && u.TempraryProject == false
                         select u;

            foreach (var img in images)
            {
                BlobConnector.RemoveImage(img);
                _db.Entry(img).State = System.Data.Entity.EntityState.Deleted;
            }

            _db.SaveChanges();


            return RedirectToAction("ProjectsListView");
        }

        private TempProject CreateEmptyTempProject()
        {
            _db = new ApplicationDbContext();
            TempProject tempProject = new TempProject();
            tempProject.DateTimeCreated = DateTime.Now;
            _db.TempProjects.Add(tempProject);
            _db.SaveChanges();

            return tempProject;
        }

        public ActionResult CreateTempProjectView()
        {
            _db = new ApplicationDbContext();
            ApplicationDbContext db2 = new ApplicationDbContext();

            TempProjectViewModel projectViewModel = Mapper.Map<TempProject, TempProjectViewModel>(CreateEmptyTempProject());
            projectViewModel.Technologies = new List<TechnologyViewModel>();

            var tempTechnologiesList = from u in _db.TempProjectTechnologies
                                       where u.ProjectId == projectViewModel.TempProjectId
                                       select u;

            foreach (var tempProjectTechnology in tempTechnologiesList)
            {
                var technology = (from z in db2.Technologies
                                  where z.TechnologyId == tempProjectTechnology.ProjectId
                                  select z).FirstOrDefault();

                if (technology == null) continue;
                var tech = Mapper.Map<Technology, TechnologyViewModel>(technology);
                projectViewModel.Technologies.Add(tech);
            }
            bool exists = Directory.Exists(Server.MapPath("~/UploadedFiles/TempProject" + projectViewModel.TempProjectId + "Data"));
            if (!exists) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/TempProject" + projectViewModel.TempProjectId + "Data"));

            return View("ProjectsMgt/CreateTempProjectView", projectViewModel);
        }

        public ActionResult EditProjectView(int projectId, string error)
        {
            ApplicationDbContext db3 = new ApplicationDbContext();
            _db = new ApplicationDbContext();
            ApplicationDbContext db2 = new ApplicationDbContext();


            var pvm = (from x in db3.Projects
                       where x.ProjectId == projectId
                       select x).FirstOrDefault();
            ProjectViewModel projectViewModel = Mapper.Map<Project, ProjectViewModel>(pvm);
            projectViewModel.Technologies = new List<TechnologyViewModel>();

            var technologiesList = from u in _db.ProjectTechnologies
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
            if (!string.IsNullOrEmpty(error)) ModelState.AddModelError("", error);


            return View("ProjectsMgt/EditProjectView", projectViewModel);
        }

        public ActionResult CreateTempProjectView2(int tempProjectId, string error)
        {
            ApplicationDbContext db3 = new ApplicationDbContext();
            _db = new ApplicationDbContext();
            ApplicationDbContext db2 = new ApplicationDbContext();


            var pvm = (from x in db3.TempProjects
                       where x.TempProjectId == tempProjectId
                       select x).FirstOrDefault();
            TempProjectViewModel projectViewModel = Mapper.Map<TempProject, TempProjectViewModel>(pvm);
            projectViewModel.Technologies = new List<TechnologyViewModel>();

            var tempTechnologiesList = from u in _db.TempProjectTechnologies
                                       where u.ProjectId == projectViewModel.TempProjectId
                                       select u;

            foreach (var tempProjectTechnology in tempTechnologiesList)
            {
                var technology = (from z in db2.Technologies
                                  where z.TechnologyId == tempProjectTechnology.TechnologyId
                                  select z).FirstOrDefault();

                if (technology == null) continue;
                var tech = Mapper.Map<Technology, TechnologyViewModel>(technology);

                projectViewModel.SelectedTechnology = null;
                projectViewModel.Technologies.Add(tech);
            }

            if (!string.IsNullOrEmpty(error)) ModelState.AddModelError("", error);

            return View("ProjectsMgt/CreateTempProjectView", projectViewModel);
        }

        public ActionResult RemoveTempProject(int tempProjectId)
        {
            _RemoveTempProject(tempProjectId);

            return RedirectToAction("TempProjectsListView");
        }

        private void _RemoveTempProject(int projectId)
        {
            _db = new ApplicationDbContext();

            var temoProject = (from u in _db.TempProjects
                               where u.TempProjectId == projectId
                               select u).FirstOrDefault();
            BlobConnector.RemoveIcon(projectId, false);
            _db.Entry(temoProject).State = System.Data.Entity.EntityState.Deleted;

            var tempProjectTechnologies = from u in _db.TempProjectTechnologies
                                          where u.ProjectId == projectId
                                          select u;

            foreach (var tpt in tempProjectTechnologies)
            {
                _db.Entry(tpt).State = System.Data.Entity.EntityState.Deleted;
            }

            var images = from u in _db.Images
                         where u.ProjectId == projectId && u.TempraryProject
                         select u;

            foreach (var img in images)
            {
                BlobConnector.RemoveImage(img);
                _db.Entry(img).State = System.Data.Entity.EntityState.Deleted;
            }

            _db.SaveChanges();
        }

        public ActionResult TempProjectsListView()
        {
            TempProjectListModelView tempProjectListModelView = new TempProjectListModelView();
            return View("ProjectsMgt/TempProjectsListView", tempProjectListModelView);
        }

        private void AddTechnologyToTempProject(int projectId, int selectedTechnology)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            TempProjectTechnology tempProjectTechnology = new TempProjectTechnology()
            {
                ProjectId = projectId,
                TechnologyId = selectedTechnology
            };
            db.TempProjectTechnologies.Add(tempProjectTechnology);
            db.SaveChanges();
        }

        private void AddTechnologyToProject(int projectId, int selectedTechnology)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            ProjectTechnology projectTechnology = new ProjectTechnology()
            {
                ProjectId = projectId,
                TechnologyId = selectedTechnology
            };
            db.ProjectTechnologies.Add(projectTechnology);
            db.SaveChanges();
        }

        public ActionResult RemoveTechnologyFromTempProject(int tempTechnologyId, int tempProjectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var tpt = (from u in db.TempProjectTechnologies
                       where u.TechnologyId == tempTechnologyId && u.ProjectId == tempProjectId
                       select u).FirstOrDefault();
            if (tpt == null) return RedirectToAction("CreateTempProjectView2", new { tempProjectId });

            db.TempProjectTechnologies.Remove(tpt);
            db.SaveChanges();

            return RedirectToAction("CreateTempProjectView2", new { tempProjectId });
        }

        public ActionResult RemoveImageFromTempProject(int imageId, int tempProjectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var image = (from u in db.Images
                         where u.ImageId == imageId
                         select u).FirstOrDefault();

            if (image == null) return RedirectToAction("CreateTempProjectView2", new { tempProjectId });

            BlobConnector.RemoveImage(image);
            db.Images.Remove(image);
            db.SaveChanges();

            return RedirectToAction("CreateTempProjectView2", new { tempProjectId });
        }

        public ActionResult RemoveTechnologyFromProject(int technologyId, int projectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var tpt = (from u in db.ProjectTechnologies
                       where u.TechnologyId == technologyId && u.ProjectId == projectId
                       select u).FirstOrDefault();
            if (tpt == null) return RedirectToAction("EditProjectView", new { projectId });

            db.ProjectTechnologies.Remove(tpt);
            db.SaveChanges();

            return RedirectToAction("EditProjectView", new { projectId });
        }

        public ActionResult RemoveImageFromProject(int imageId, int projectId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var image = (from u in db.Images
                         where u.ImageId == imageId
                         select u).FirstOrDefault();

            if (image == null) return RedirectToAction("EditProjectView", new { projectId });
            BlobConnector.RemoveImage(image);
            db.Images.Remove(image);
            db.SaveChanges();

            return RedirectToAction("EditProjectView", new { projectId });
        }

        public ActionResult UpdateTempProject(TempProjectViewModel tempProjectViewModel)
        {
            _db = new ApplicationDbContext();
            var tempProject = Mapper.Map<TempProjectViewModel, TempProject>(tempProjectViewModel);

            tempProject.DateTimeCreated = DateTime.Now;
            tempProject.ShowInCv = false;
            tempProject.AuthorId = User.Identity.GetUserId();


            _db.Entry(tempProject).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            if (tempProjectViewModel.SelectedTechnology != null)
            {
                AddTechnologyToTempProject(tempProjectViewModel.TempProjectId, tempProjectViewModel.SelectedTechnology.GetValueOrDefault());
            }

            return RedirectToAction("CreateTempProjectView2", new { tempProjectId = tempProjectViewModel.TempProjectId });
        }

        [HttpPost]
        public ActionResult UploadTempProjImage(HttpPostedFileBase file, int tempProjectId)
        {
            _db = new ApplicationDbContext();

            Image image = new Image()
            {
                OriginalFileName = file.FileName,
                FileName = "TempProject" + tempProjectId + file.FileName,
                ProjectId = tempProjectId,
                TempraryProject = true
            };

            BlobConnector.UploadImage(file, image);
            _db.Images.Add(image);
            _db.SaveChanges();

            return RedirectToAction("CreateTempProjectView2", new { TempProjectId = tempProjectId });
        }

        [HttpPost]
        public ActionResult UploadProjImage(HttpPostedFileBase file, int projectId)
        {
            _db = new ApplicationDbContext();

            Image image = new Image()
            {
                OriginalFileName = file.FileName,
                FileName = "Project" + projectId + file.FileName,
                ProjectId = projectId,
                TempraryProject = false
            };

            BlobConnector.UploadImage(file, image);
            _db.Images.Add(image);
            _db.SaveChanges();

            return RedirectToAction("EditProjectView", new { projectId });
        }

        public ActionResult EducationMgtView()
        {
            EducationMgtViewModel educationMgtViewModel = new EducationMgtViewModel();
            return View("EducationMgt/EducationMgtView", educationMgtViewModel);
        }

        public ActionResult AddEducationToCv(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Educations
                       where u.EducationId == cvModel.SelectedEducation
                       select u).FirstOrDefault();

            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = true;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult UpdateEducation(EducationViewModel educationViewModel)
        {
            _db = new ApplicationDbContext();
            Education education = Mapper.Map<EducationViewModel, Education>(educationViewModel);


            _db.Entry(education).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EducationMgtView");
        }

        public ActionResult RemoveEducationFromCv(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Educations
                       where u.EducationId == id
                       select u).FirstOrDefault();

            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = false;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult AddEducation(EducationViewModel educationView)
        {
            Education education = Mapper.Map<EducationViewModel, Education>(educationView);

            ApplicationDbContext db = new ApplicationDbContext();

            db.Educations.Add(education);
            db.SaveChanges();

            return RedirectToAction("EducationMgtView");
        }

        public ActionResult RemoveEducation(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Educations
                       where u.EducationId == id
                       select u).FirstOrDefault();

            _db.Entry(cvm).State = System.Data.Entity.EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("EducationMgtView");
        }

        public ActionResult EmploymentHistoryMgtView()
        {
            EmploymentHistoryMgtViewModel employmentHistoryMgtViewModel = new EmploymentHistoryMgtViewModel();
            return View("EmploymentHistoryMgt/EmploymentHistoryView", employmentHistoryMgtViewModel);
        }

        public ActionResult AddEmploymentHistroyToCv(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.EmploymentHistories
                       where u.EmploymentHistoryId == cvModel.SelectedEmploymentHistory
                       select u).FirstOrDefault();

            if (cvm == null) return RedirectToAction("EditCvPage");


            cvm.ShowInCv = true;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult UpdateEmploymentHistroy(EmploymentHistoryViewModel employmentHistoryViewModel)
        {
            _db = new ApplicationDbContext();
            EmploymentHistory employmentHistory = Mapper.Map<EmploymentHistoryViewModel, EmploymentHistory>(employmentHistoryViewModel);


            _db.Entry(employmentHistory).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EmploymentHistoryMgtView");
        }

        public ActionResult RemoveEmploymentHistroyFromCv(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.EmploymentHistories
                       where u.EmploymentHistoryId == id
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = false;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult AddEmploymentHistory(EmploymentHistoryViewModel employmentHistoryViewModel)
        {
            EmploymentHistory employmentHistory = Mapper.Map<EmploymentHistoryViewModel, EmploymentHistory>(employmentHistoryViewModel);

            ApplicationDbContext db = new ApplicationDbContext();

            db.EmploymentHistories.Add(employmentHistory);
            db.SaveChanges();

            return RedirectToAction("EmploymentHistoryMgtView");
        }

        public ActionResult RemoveEmploymentHistory(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.EmploymentHistories
                       where u.EmploymentHistoryId == id
                       select u).FirstOrDefault();

            _db.Entry(cvm).State = System.Data.Entity.EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("EmploymentHistoryMgtView");
        }

        public ActionResult AdditionalInformationMgtView()
        {
            AdditionalInformationMgtViewModel additionalInformationMgtViewModel = new AdditionalInformationMgtViewModel();
            return View("AdditionalInformationMgt/AdditionalInformationMgtView", additionalInformationMgtViewModel);
        }

        public ActionResult AddAdditionalInfoToCv(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.AdditionalInfos
                       where u.AdditionalInfoId == cvModel.SelectedAddtinionaInformation
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = true;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult RemoveAdditionalInformationFromCv(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.AdditionalInfos
                       where u.AdditionalInfoId == id
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = false;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult RemoveAdditionalInformation(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.AdditionalInfos
                       where u.AdditionalInfoId == id
                       select u).FirstOrDefault();

            _db.Entry(cvm).State = System.Data.Entity.EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("AdditionalInformationMgtView");
        }

        public ActionResult AddAdditionalInformation(AdditionalInfoViewModel additionalInfoViewModel)
        {
            AdditionalInfo additionalInfo = Mapper.Map<AdditionalInfoViewModel, AdditionalInfo>(additionalInfoViewModel);

            ApplicationDbContext db = new ApplicationDbContext();

            db.AdditionalInfos.Add(additionalInfo);
            db.SaveChanges();

            return RedirectToAction("AdditionalInformationMgtView");
        }

        public ActionResult AchivmentsEditView()
        {
            AchivmentViewModel achivmentViewModel = new AchivmentViewModel();
            return View("AchivmentsMgt/AchivmentsEditView", achivmentViewModel);
        }

        public ActionResult AddAchivmentToCv(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Achivments
                       where u.AchivmentId == cvModel.SelectedAchivment
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = true;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult RemoveAchivmentFromCv(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Achivments
                       where u.AchivmentId == id
                       select u).FirstOrDefault();

            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = false;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult RemoveAchivment(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Achivments
                       where u.AchivmentId == id
                       select u).FirstOrDefault();

            _db.Entry(cvm).State = System.Data.Entity.EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction("AchivmentsEditView");
        }

        public ActionResult AddAchivment(AchivmentViewModel achivment)
        {
            Achivment achiv = Mapper.Map<AchivmentViewModel, Achivment>(achivment);

            ApplicationDbContext db = new ApplicationDbContext();


            db.Achivments.Add(achiv);
            db.SaveChanges();

            return RedirectToAction("AchivmentsEditView");
        }

        public ActionResult AddToCvVeryWellKnowTechnology(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Technologies
                       where u.TechnologyId == cvModel.SelectedTechnology
                       select u).FirstOrDefault();

            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = true;
            cvm.LevelOfKnowledge = 0;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult AddToCvWellKnowTechnology(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Technologies
                       where u.TechnologyId == cvModel.SelectedTechnology
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = true;
            cvm.LevelOfKnowledge = 1;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult AddToCvKnowTechnology(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Technologies
                       where u.TechnologyId == cvModel.SelectedTechnology
                       select u).FirstOrDefault();

            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = true;
            cvm.LevelOfKnowledge = 2;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult RemoveTechnologyFromCv(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Technologies
                       where u.TechnologyId == id
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = false;
            cvm.LevelOfKnowledge = 3;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult TechnologiesView()
        {

            TechnologyListViewModel technologyListViewModel = new TechnologyListViewModel();
            return View("TechnologiesMgt/TechnologiesView", technologyListViewModel);
        }

        public ActionResult AddTechnology(TechnologyViewModel technology)
        {
            Technology tech = Mapper.Map<TechnologyViewModel, Technology>(technology);

            ApplicationDbContext db = new ApplicationDbContext();

            tech.ShowInCv = false;
            tech.LevelOfKnowledge = 2;

            db.Technologies.Add(tech);
            db.SaveChanges();


            return RedirectToAction("TechnologiesView");
        }

        public ActionResult RemoveTechnology(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Technologies
                       where u.TechnologyId == id
                       select u).FirstOrDefault();

            _db.Entry(cvm).State = System.Data.Entity.EntityState.Deleted;
            _db.SaveChanges();


            return RedirectToAction("TechnologiesView");
        }

        public ActionResult UpdatePrivateInformation(PrivateInformationViewModel privateInformationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditCvPages", privateInformationViewModel);


            }

            PrivateInformation privateInformation = Mapper.Map<PrivateInformationViewModel, PrivateInformation>(privateInformationViewModel);
            ApplicationDbContext db = new ApplicationDbContext();

            db.Entry(privateInformation).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EditCvPage");

        }

        public ActionResult AddProjectToCv(CvViewModel cvModel)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Projects
                       where u.ProjectId == cvModel.SelectedProject
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = true;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult RemoveProjectFromCv(int id)
        {
            _db = new ApplicationDbContext();

            var cvm = (from u in _db.Projects
                       where u.ProjectId == id
                       select u).FirstOrDefault();
            if (cvm == null) return RedirectToAction("EditCvPage");

            cvm.ShowInCv = false;
            _db.Entry(cvm).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("EditCvPage");
        }

        public ActionResult EditCvPage()
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

            return View("cvmgt/Index", _cvViewModel);
        }

        public ActionResult EditCvPages(PrivateInformationViewModel privateInformationViewModel)
        {
            _cvViewModel = new CvViewModel();
            _db = new ApplicationDbContext();

            _cvViewModel.PrivateInformationViewModel = privateInformationViewModel;

            GetAchivments();
            GetAdditionalInfos();
            GetEducations();
            GetEmploymentHistories();
            GetProjects();
            GetTechnologies();

            return View("cvmgt/Index", _cvViewModel);
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
            _cvViewModel.Projects = new List<ProjectViewModel>();

            var cvm = from u in _db.Projects
                      select u;

            foreach (Project a in cvm)
            {
                var j = Mapper.Map<Project, ProjectViewModel>(a);
                _cvViewModel.Projects.Add(j);
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

        public ActionResult UserMgt(int? page = null, string login = null, string firstName = null, string lastName = null, string email = null, bool? blocked = null)
        {
            page = page.GetValueOrDefault();
            UserListViewModel ulv = new UserListViewModel(page);

            if (page < 0)
            {
                page = 0;
            }
            else if (page > ulv.MaxPage())
            {
                page--;
            }

            ulv.List = UserListViewModel.GetUserList(page.GetValueOrDefault(), login, firstName, lastName, email, blocked);
            ulv.Page = page.GetValueOrDefault();

            if (!string.IsNullOrEmpty(login) || !string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName) || !string.IsNullOrEmpty(email)) ulv.Filters = "<h3>Filtry: </h3>";

            if (!string.IsNullOrEmpty(login)) ulv.Filters += "<b>Login: </b>" + login;
            if (!string.IsNullOrEmpty(firstName)) ulv.Filters += " <b>First name: </b>" + firstName;
            if (!string.IsNullOrEmpty(lastName)) ulv.Filters += " <b>Last name: </b>" + lastName;
            if (!string.IsNullOrEmpty(email)) ulv.Filters += " <b>E-mail: </b>" + email;

            if (Request.IsAjaxRequest())
            {
                return PartialView("UserMgt/_UserListPartial", ulv);
            }
            return View("UserMgt/UserMgtView", ulv);
        }


        public ActionResult Home()
        {

            return View();
        }

        public ActionResult EditAboutMe()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            AboutMe aboutMe = (from u in db.AboutMe
                               select u).FirstOrDefault();

            // if (rol == null) return RedirectToAction("RoleMgt", new { page = 0 });

            AboutMeViewModel aboutMeViewModel = Mapper.Map<AboutMe, AboutMeViewModel>(aboutMe);

            // _db.Entry(rol).State = System.Data.Entity.EntityState.Deleted;
            // _db.SaveChanges();

            return View("AboutMeMgt/AboutMeEditView", aboutMeViewModel);
        }

        public ActionResult UpdateAboutMe(AboutMeViewModel aboutMeViewModel)
        {
            //AboutMeViewModel aboutMeViewModel = new AboutMeViewModel();
            ApplicationDbContext db = new ApplicationDbContext();

            AboutMe aboutMe = Mapper.Map<AboutMeViewModel, AboutMe>(aboutMeViewModel);
            aboutMe.AboutMeId = 1;
            db.Entry(aboutMe).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EditAboutMe");
        }

        public ActionResult EditContact()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Contact contact = (from u in db.Contact
                               select u).FirstOrDefault();

            ContactViewModel contactViewModel = Mapper.Map<Contact, ContactViewModel>(contact);

            return View("ContactMgt/EditContactData", contactViewModel);
        }

        public ActionResult UpdateContact(ContactViewModel contactViewModel)
        {
            //AboutMeViewModel aboutMeViewModel = new AboutMeViewModel();
            ApplicationDbContext db = new ApplicationDbContext();

            Contact contact = Mapper.Map<ContactViewModel, Contact>(contactViewModel);
            contact.ContactId = 1;
            db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EditContact");
        }
    }
}