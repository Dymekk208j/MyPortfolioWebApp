using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class AdditionalInfoViewModel
    {

        public int AdditionalInfoId { get; set; }
        public int Type { get; set; } //0 - jezyki obce, 1 - dodatkowe umiejetnosci, 2 - zainteresowania
        [Display(Name = "Typ")]
        public List<StrType> TypeList { get; set; } //0 - jezyki obce, 1 - dodatkowe umiejetnosci, 2 - zainteresowania

        [Display(Name = "Tytuł")]
        public String Title { get; set; }

        [Display(Name = "Dodaj do CV")]
        public bool ShowInCv { get; set; }
        public AdditionalInfoViewModel()
        {
            TypeList = new List<StrType>();
            TypeList.Add(new StrType(0, "Język obcy"));
            TypeList.Add(new StrType(1, "Dodatkowa umiejętność"));
            TypeList.Add(new StrType(2, "Zainteresowanie"));           
        }

        public class StrType
        {
            public int Id { get; set; }
            public String Name { get; set; }
           
                public StrType(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}