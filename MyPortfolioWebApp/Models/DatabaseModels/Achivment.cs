using System;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Achivment
    {
        public int AchivmentId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime Date { get; set; }
        public bool ShowInCv { get; set; }
    }
}