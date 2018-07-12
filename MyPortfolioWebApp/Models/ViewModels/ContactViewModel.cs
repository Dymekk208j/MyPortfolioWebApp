using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyPortfolioWebApp.Models.DatabaseModels;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }

        [Display(Name = "Link do profilu na portalu GitHub")]
        [Required(ErrorMessage = "Link do profilu na githuba jest wymagany")]
        public String GitHubLink { get; set; }

        [Display(Name = "Link do profilu na portali LinkedIn")]
        [Required(ErrorMessage = "Link do profilu na LinkedIn jest wymagany")]
        public String LinkedInLink { get; set; }

        [Display(Name = "Link do profilu na portalu Facebook")]
        public String FacebookLink { get; set; }

        [Display(Name = "Głowny adres e-mail")]
        [Required(ErrorMessage = "Głowny adres e-mail jest wymagany")]
        public String Email1 { get; set; }

        [Display(Name = "Pomocniczy adres e-mail")]
        public String Email2 { get; set; }

        [Display(Name = "Numer telefonu")]
        public String PhoneNumber { get; set; }

        public ContactViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Contact con = (from u in db.Contact
                           select u).FirstOrDefault();
            if (con != null)
            {
                GitHubLink = con.GitHubLink;
                LinkedInLink = con.LinkedInLink;
                FacebookLink = con.FacebookLink;
                Email1 = con.Email1;
                Email2 = con.Email2;
                PhoneNumber = con.PhoneNumber;
            }
        }
    }
}