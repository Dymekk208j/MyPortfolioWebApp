using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class EducationMgtViewModel
    {
        public List<EducationViewModel> List { get;  }

        public EducationMgtViewModel()
        {
            List = new List<EducationViewModel>();
            ApplicationDbContext db = new ApplicationDbContext();

            var edList = from u in db.Educations select u;
            EducationViewModel addEdu;
            foreach (Education education in edList)
            {
                addEdu = Mapper.Map<Education, EducationViewModel>(education);

                List.Add(addEdu);
            }
        }
    }    
}