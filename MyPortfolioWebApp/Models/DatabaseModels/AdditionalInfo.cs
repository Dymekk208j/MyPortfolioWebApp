using System;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class AdditionalInfo
    {
        public int AdditionalInfoId { get; set; }
        public int Type { get; set; } //0 - jezyki obce, 1 - dodatkowe umiejetnosci, 2 - zainteresowania
        public String Title { get; set; }
        public bool ShowInCv { get; set; }

    }
}