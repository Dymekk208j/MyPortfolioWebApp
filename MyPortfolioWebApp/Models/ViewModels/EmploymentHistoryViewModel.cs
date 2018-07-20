using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class EmploymentHistoryViewModel
    {
        public int EmploymentHistoryId { get; set; }
        [Required]
        [Display(Name = "Nazwa firmy")]
        public string CompanyName { get; set; }

        [Required]

        [Display(Name = "Stanowisko")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        public string CityOfEmployment { get; set; }

        [Required]
        [Display(Name = "Data rozpoczęcia")]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Data zakończenia")]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Aktualne miejsce zatrudnienia?")]
        public bool CurrentPlaceOfEmployment { get; set; }

        [Required]
        [Display(Name = "Dodaj co CV")]
        public bool ShowInCv { get; set; }
    }
}