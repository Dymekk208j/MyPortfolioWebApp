using AutoMapper;
using MyPortfolioWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Project
    {
        public int ProjectId { get; set; }
        public String Title { get; set; }
        public String ShordDescription { get; set; }
        public String FullDescription { get; set; }
        public bool Commercial { get; set; }
        public bool ShowInCv { get; set; }
        public String ImageLink { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string AuthorId { get; set; }
        public List<TechnologyViewModel> GetTechnologies(int projectId)
        {
            List<TechnologyViewModel> technologies = new List<TechnologyViewModel>();
            ApplicationDbContext db = new ApplicationDbContext();

            var projectTechnologiesList = (from u in db.ProjectTechnologies
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
    }
}