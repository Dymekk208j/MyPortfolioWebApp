using MyPortfolioWebApp.Models.DatabaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class TechnologyViewModel
    {
   
        public int? TechnologyId { get; set; }

        [Display(Name = "Nazwa technologii")]
        public string Name { get; set; }

    }
}