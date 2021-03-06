﻿using MyPortfolioWebApp.Models.DatabaseModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class PrivateInformationViewModel
    {
        public int PrivateInformationId { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Musisz wprowadzić imię")]
        [StringLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Musisz wprowadzić nazwisko")]
        [StringLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków")]
        public string LastName { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Musisz wprowadzić ulicę")]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Musisz wprowadzić miasto")]
        [StringLength(50, ErrorMessage = "Miasto może mieć maksymalnie 50 znaków")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Musisz wprowadzić kod pocztowy")]
        [StringLength(6, ErrorMessage = "Kod może mieć maksymalnie 6 znaków")]
        public string PostCode { get; set; }

        [Display(Name = "Numer domu")]
        [Required(ErrorMessage = "Musisz wprowadzić numer domu")]
        public string HouseNumber { get; set; }

        [Display(Name = "Numer mieszkania")]
        [Required(ErrorMessage = "Musisz wprowadzić numer mieszkania")]
        public string FlatNumber { get; set; }

        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "Musisz wprowadzić numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adres strony internetowej")]
        [Required(ErrorMessage = "Musisz wprowadzić adres strony internetowej")]
        public string HomePage { get; set; }

        [Display(Name = "Adres e-mail")]
        [Required(ErrorMessage = "Musisz wprowadzić adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Link do zdjęcia")]
        public string ImageLink { get; set; }


        public PrivateInformationViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            PrivateInformation pi = (from u in db.PrivateInformation
                           select u).FirstOrDefault();

            if (pi == null) return;
            City = pi.City;
            Email = pi.Email;
            FirstName = pi.FirstName;
            LastName = pi.LastName;
            ImageLink = pi.ImageLink;
            HomePage = pi.HomePage;
            HouseNumber = pi.HouseNumber;
            FlatNumber = pi.FlatNumber;
            PrivateInformationId = pi.PrivateInformationId;
            Street = pi.Street;
            PostCode = pi.PostCode;     
            PhoneNumber = pi.PhoneNumber;
        }
    }
}