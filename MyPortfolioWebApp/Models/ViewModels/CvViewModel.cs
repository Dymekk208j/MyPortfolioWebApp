using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;
using System.Collections.Generic;





using System.Linq;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class CvViewModel
    {
        public int SelectedProject { get; set; }
        public int SelectedTechnology { get; set; }
        public int SelectedAchivment { get; set; }
        public int SelectedAddtinionaInformation { get; set; }
        public int SelectedEmploymentHistory { get; set; }
        public int SelectedEducation { get; set; }

        public PrivateInformationViewModel PrivateInformationViewModel { get; set; }

        public List<Achivment> Achivments { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
        public List<Education> Educations { get; set; }
        public List<EmploymentHistory> EmploymentHistories { get; set; }
        public List<ProjectViewModel> Projects { get; set; }
        public List<Technology> Technologies{ get; set; }

        public CvViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            PrivateInformation privateInformation = (from u in db.PrivateInformation
                                  select u).FirstOrDefault();
            PrivateInformationViewModel = Mapper.Map<PrivateInformation, PrivateInformationViewModel>(privateInformation);
        }
        //public List<TechnologyViewModel> GetTechnologies(int projectId)
        //{
        //    List<TechnologyViewModel> technologiesList = new List<TechnologyViewModel>();
        //    ApplicationDbContext db = new ApplicationDbContext();

        //    var projectTechnologiesList = (from u in db.ProjectTechnologies
        //                                   where u.ProjectId == projectId
        //                                   select u);
        //    ApplicationDbContext db2 = new ApplicationDbContext();
        //    TechnologyViewModel tvm;
        //    foreach (var t in projectTechnologiesList)
        //    {
        //        var technology = (from u in db2.Technologies
        //                          where u.TechnologyId == t.TechnologyId
        //                          select u).FirstOrDefault();
        //        tvm = Mapper.Map<Technology, TechnologyViewModel>(technology);
        //        if (tvm != null) technologiesList.Add(tvm);
        //    }
        //    return technologiesList;

        //}


    }
}