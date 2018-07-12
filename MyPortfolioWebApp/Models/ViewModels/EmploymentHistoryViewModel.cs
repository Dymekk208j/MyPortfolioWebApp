using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class EmploymentHistoryViewModel
    {
        public int EmploymentHistoryId { get; set; }
        [Required]
        [Display(Name = "Nazwa firmy")]
        public String CompanyName { get; set; }

        [Required]

        [Display(Name = "Stanowisko")]
        public String Position { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        public String CityOfEmployment { get; set; }

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

        public String StrStartDate => DateTime.ParseExact(StartDate.ToShortDateString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        public String StrEndDate => DateTime.ParseExact(EndDate.ToShortDateString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}