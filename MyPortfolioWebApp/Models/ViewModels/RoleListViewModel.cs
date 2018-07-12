using System.Collections.Generic;
using System.Linq;
using MyPortfolioWebApp.Models.DatabaseModels;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class RoleListViewModel
    {
        //public List<RoleViewModel> List;
        //public int? Page;
        //public bool IsNextPage { get; }
        //public bool IsPreviousPage { get; }       

        //public RoleListViewModel(int? page)
        //{
        //    Page = page;
        //    page = page.GetValueOrDefault();
        //    IsNextPage = !(page + 1 > MaxPage());

        //    IsPreviousPage = !(page - 1 < 0);

        //}

        //private static int RoleAmount()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var amount = from u in db.Roles
        //                 select u;

        //    return amount.Count();
        //}

        //public int MaxPage()
        //{
        //    return RoleAmount() / 10;
        //}
    }
}