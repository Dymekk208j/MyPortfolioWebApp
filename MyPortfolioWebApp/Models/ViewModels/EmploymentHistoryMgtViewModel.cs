using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class EmploymentHistoryMgtViewModel
    {
        public List<EmploymentHistoryViewModel> List { get; set; }

        public EmploymentHistoryMgtViewModel()
        {
            List = new List<EmploymentHistoryViewModel>();
            ApplicationDbContext db = new ApplicationDbContext();

            var empList = from u in db.EmploymentHistories select u;
            foreach (EmploymentHistory employmentHistory in empList)
            {
                var employmentHist = Mapper.Map<EmploymentHistory, EmploymentHistoryViewModel>(employmentHistory);

                List.Add(employmentHist);
            }

        }
    }
}