using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Education
    {
        public int EducationId { get; set; }
        public String SchooleName { get; set; }
        public String Department { get; set; }
        public String Specialization { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }
        public bool CurrentPlaceOfEducation { get; set; }
        public bool ShowInCv { get; set; }
    }
}