using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyPortfolioWebApp.Models.DatabaseModels;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class AchivmentViewModel
    {
        public List<Achivment> Achivments { get; set; }

        [Display(Name = "Opis osiągnięcia")]
        public String Description { get; set; }

        [Display(Name = "Data osiągnięcia")]
        public DateTime Date { get; set; }

        [Display(Name = "Tytuł osiągnięcia")]
        public String Title { get; set; }

        [Display(Name = "Dodaj co CV")]
        public bool ShowInCv { get; set; }


        public AchivmentViewModel()
        {
            Achivments = new List<Achivment>();
            ApplicationDbContext db = new ApplicationDbContext();

            var achiv = from u in db.Achivments
                       select u;

            foreach (var a in achiv)
            {
                Achivments.Add(a);
            }
        }
    }
}