using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using MyPortfolioWebApp.Models;
using MyPortfolioWebApp.Models.DatabaseModels;
using MyPortfolioWebApp.Models.ViewModels;

namespace MyPortfolioWebApp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Contact contact = (from u in db.Contact
                               select u).FirstOrDefault();

            ContactViewModel contactViewModel = Mapper.Map<Contact, ContactViewModel>(contact);
            return View(contactViewModel);
        }
    }
}