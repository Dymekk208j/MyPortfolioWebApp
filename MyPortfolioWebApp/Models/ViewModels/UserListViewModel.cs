using System;
using System.Collections.Generic;
using System.Linq;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class UserListViewModel
    {
        public List<ApplicationUser> List;
        public int? Page;
        public String Filters;

        public bool IsNextPage { get; }
        public bool IsPreviousPage { get; }

        public UserListViewModel(int? page)       {
            Page = page;
            page = page.GetValueOrDefault();

            IsNextPage = !(page + 1 > MaxPage());
            IsPreviousPage = !(page - 1 < 0);

        }
        
        private static int UsersAmount()
        {
            var db = new ApplicationDbContext();
            return db.Users.ToList().Count();
        }

        public int MaxPage()
        {
            return UsersAmount() / 10;
        }

        public static List<ApplicationUser> GetUserList(int? page = null, string login = null, string firstName = null, string lastName = null, string email = null, bool? blocked = null)
        {

            List<ApplicationUser> userList = new List<ApplicationUser>();
            ApplicationDbContext db = new ApplicationDbContext();

            var usr = from z in db.Users select z;


            if (!string.IsNullOrEmpty(login)) usr = usr.Where(d => d.UserName == login);
            if (!string.IsNullOrEmpty(firstName)) usr = usr.Where(d => d.FirstName == firstName);
            if (!string.IsNullOrEmpty(lastName)) usr = usr.Where(d => d.LastName == lastName);
            if (!string.IsNullOrEmpty(email)) usr = usr.Where(d => d.Email == email);
            if (blocked != null) usr = usr.Where(d => d.Blocked == blocked);
            if (page < 0) page = 0;

            usr = usr.OrderBy(d => d.Id);
            usr = usr.Skip(page.GetValueOrDefault() * 10).Take(10);

            foreach (var us in usr)
            {
                userList.Add(us);
            }

            return userList;
        }
    }
}