using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class EducationViewModel
    {
        public int EducationId { get; set; }
        [Display(Name = "Nazwa szkoły/uczelni")]
        public string SchooleName { get; set; }

        [Display(Name = "Poziom szkoły/Wydział")]
        public string Department { get; set; }

        [Display(Name = "Specjalizacja")]
        public string Specialization { get; set; }

        [Display(Name = "Data rozpoczęcia")]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Data zakończenia")]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Aktualne miejsce nauki")]
        public bool CurrentPlaceOfEducation { get; set; }

        [Display(Name = "Dodaj do CV")]
        public bool ShowInCv { get; set; }
        }
}