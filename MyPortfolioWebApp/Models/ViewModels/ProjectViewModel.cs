using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        public int? SelectedTechnology { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Krótki opis")]
        public string ShordDescription { get; set; }

        [Display(Name = "Pełen opis")]
        public string FullDescription { get; set; }

        [Display(Name = "Projekt komercyjny")]
        public bool Commercial { get; set; }

        [Display(Name = "Czy została dodana ikona")]
        public bool IsIcon { get; set; }

        public List<TechnologyViewModel> Technologies { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string AuthorId { get; set; }
        public bool ShowInCv { get; set; }

        public static List<Image> GetImagesForProject(int projectId)
        {
            List<Image> images = new List<Image>();
            ApplicationDbContext db = new ApplicationDbContext();
            var imgList = from z in db.Images
                          where z.ProjectId == projectId && z.TempraryProject == false
                          select z;

            foreach (var v in imgList)
            {
                if (v != null) images.Add(v);
            }

            return images;
        }
        public List<TechnologyViewModel> GetTechnologies(int projectId)
        {
            List<TechnologyViewModel> technologies = new List<TechnologyViewModel>();
            ApplicationDbContext db = new ApplicationDbContext();

            var projectTechnologiesList = (from u in db.ProjectTechnologies
                                           where u.ProjectId == projectId
                                           select u);
            ApplicationDbContext db2 = new ApplicationDbContext();
            foreach (var t in projectTechnologiesList)
            {
                var technology = (from u in db2.Technologies
                                  where u.TechnologyId == t.TechnologyId
                                  select u).FirstOrDefault();
                var tvm = Mapper.Map<Technology, TechnologyViewModel>(technology);
                if (tvm != null) technologies.Add(tvm);
            }
            return technologies;
        }

        public static List<TechnologyViewModel> GetRemaningTechnologies(ProjectViewModel projectViewModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<int> technologiesInProject = new List<int>();

            var tech = from u in db.ProjectTechnologies
                where u.ProjectId == projectViewModel.ProjectId
                select u;
            foreach (var i in tech)
            {
                if (i != null) technologiesInProject.Add(i.TechnologyId);
            }

            List<TechnologyViewModel> techList = new List<TechnologyViewModel>();
            techList.Add(new TechnologyViewModel()
            {
                Name = "Wybierz technologię",
                TechnologyId = null

            });

            var technologiesList = (from u in db.Technologies
                select u);

            foreach (var t in technologiesList)
            {
                var tvm = Mapper.Map<Technology, TechnologyViewModel>(t);

                if (projectViewModel.Technologies.Count > 0)
                {
                    if (!technologiesInProject.Contains(tvm.TechnologyId.GetValueOrDefault())) techList.Add(tvm);
                }
                else techList.Add(tvm);
            }
            return techList;
        }
    }
}