using System.Collections.Generic;
using System.Linq;
using MyPortfolioWebApp.Models.DatabaseModels;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class TechnologyListViewModel
    {
        public List<Technology> List { get; set; }

        public TechnologyListViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List = (from i in db.Technologies select i).ToList();
        }
    }
}