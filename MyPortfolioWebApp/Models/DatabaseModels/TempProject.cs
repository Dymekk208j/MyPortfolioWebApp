using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MyPortfolioWebApp.Models.ViewModels;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class TempProject
    {
        public int TempProjectId { get; set; }
        public string Title { get; set; }
        public string ShordDescription { get; set; }
        public string FullDescription { get; set; }
        public bool Commercial { get; set; }
        public bool ShowInCv { get; set; }
        public bool IsIcon { get; set; }

        public DateTime DateTimeCreated { get; set; }
        public string AuthorId { get; set; }

        public List<TechnologyViewModel> GetTechnologies(int projectId)
        {
            List<TechnologyViewModel> technologies = new List<TechnologyViewModel>();
            ApplicationDbContext db = new ApplicationDbContext();

            var projectTechnologiesList = (from u in db.TempProjectTechnologies
                where u.ProjectId == projectId
                select u);
            ApplicationDbContext db2 = new ApplicationDbContext();
            TechnologyViewModel tvm;
            foreach (var t in projectTechnologiesList)
            {
                var technology = (from u in db2.Technologies
                    where u.TechnologyId == t.TechnologyId
                    select u).FirstOrDefault();
                tvm = Mapper.Map<Technology, TechnologyViewModel>(technology);
                if (tvm != null) technologies.Add(tvm);
            }
            return technologies;

        }

        public static string GetIconLink(int tempProjectId)
        {
            return @"https://damiandziuraportfolio.blob.core.windows.net/icons/" + GetIconName(tempProjectId);
        }

        public static string GetIconName(int tempProjectId)
        {
            return "TempProject" + tempProjectId + "Icon.png";
        }


    }
}