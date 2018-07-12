using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class TempProjectViewModel
    {
        public int TempProjectId { get; set; }
        
        public int? SelectedTechnology { get; set; }
        [Display(Name = "Tytuł")]
        public String Title { get; set; }

        [Display(Name = "Krótki opis")]
        public String ShordDescription { get; set; }

        [Display(Name = "Pełen opis")]
        public String FullDescription { get; set; }

        [Display(Name = "Projekt komercyjny")]
        public bool Commercial { get; set; }

        [Display(Name = "Link do obrazka")]
        public String ImageLink { get; set; }

        public List<TechnologyViewModel> Technologies { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string AuthorId { get; set; }
      
        public static List<Image> GetImagesForProject(int projectId)
        {
            List<Image> images = new List<Image>();
            ApplicationDbContext db = new ApplicationDbContext();
            var imgList = from z in db.Images
                          where z.ProjectId == projectId && z.TempraryProject
                          select z;

            foreach (var v in imgList)
            {
                if (v != null) images.Add(v);
            }

            return images;
        }

        public static string GetPath(int tempProjectId)
        {
            return "/UploadedFiles/TempProject" + tempProjectId + "Data/";
        }
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

        public static List<TechnologyViewModel> GetRemaningTechnologies(TempProjectViewModel tempProjectViewModel)
        {
            List<TechnologyViewModel> techList = new List<TechnologyViewModel>();
            techList.Add(new TechnologyViewModel()
            {
                Name = "Wybierz technologię",
                TechnologyId = null

            });
            ApplicationDbContext db = new ApplicationDbContext();

            var technologiesList = (from u in db.Technologies
                                    select u);

            TechnologyViewModel tvm;
            foreach (var t in technologiesList)
            {

                tvm = Mapper.Map<Technology, TechnologyViewModel>(t);

                if (tempProjectViewModel.Technologies.Count > 0)
                {
                    if (!tempProjectViewModel.Technologies.Contains(tvm)) techList.Add(tvm);
                }
                else techList.Add(tvm);

            }
            return techList;
        }


    }
}