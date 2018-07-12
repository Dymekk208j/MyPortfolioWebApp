using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class EmploymentHistory
    {
        public int EmploymentHistoryId { get; set; }
        public String CompanyName { get; set; }
        public String Position { get; set; }
        public String CityOfEmployment { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }
        public bool CurrentPlaceOfEmployment { get; set; }

        public bool ShowInCv { get; set; }
    }
}