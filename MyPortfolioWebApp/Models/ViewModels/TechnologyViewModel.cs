using MyPortfolioWebApp.Models.DatabaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class TechnologyViewModel
    {
        public List<Technology> Technologies{ get; }
        public int? TechnologyId { get; set; }

        [Display(Name = "Nazwa technologii")]
        public string Name { get; set; }

        public TechnologyViewModel()
        {
            Technologies = new List<Technology>();
            ApplicationDbContext db = new ApplicationDbContext();

            var tech = from u in db.Technologies
                       select u;

            foreach (var a in tech)
            {
                Technologies.Add(a);
            }
        }
    }
}