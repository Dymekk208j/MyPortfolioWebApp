using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Education
    {
        public int EducationId { get; set; }
        public string SchooleName { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }
        public bool CurrentPlaceOfEducation { get; set; }
        public bool ShowInCv { get; set; }
    }
}