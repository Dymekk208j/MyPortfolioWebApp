using System;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class AboutMe
    {
        public int AboutMeId { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public String ImageLink { get; set; }
    }
}