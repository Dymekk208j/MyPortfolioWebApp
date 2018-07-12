﻿using AutoMapper;
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

        public int AuthorId { get; set; }

        public static string GetPath(int projectId)
        {
            return "/UploadedFiles/Project" + projectId + "Data/";
        }

        public static List<Image> GetImagesForProject(int projectId)
        {
            List<Image> Images = new List<Image>();
            ApplicationDbContext db = new ApplicationDbContext();
            var imgList = from z in db.Images
                          where z.ProjectId == projectId && z.TempraryProject == false
                          select z;

            foreach (var v in imgList)
            {
                if (v != null) Images.Add(v);
            }

            return Images;
        }
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

        public List<TechnologyViewModel> GetRemaningTechnologies(ProjectViewModel projectViewModel)
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

                if (projectViewModel.Technologies.Count > 0)
                {
                    if (!projectViewModel.Technologies.Contains(tvm)) techList.Add(tvm);
                }
                else techList.Add(tvm);

            }
            return techList;
        }


    }
}