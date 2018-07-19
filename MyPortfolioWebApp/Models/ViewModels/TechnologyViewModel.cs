using System.ComponentModel.DataAnnotations;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class TechnologyViewModel
    {
   
        public int? TechnologyId { get; set; }

        [Display(Name = "Nazwa technologii")]
        public string Name { get; set; }

    }
}