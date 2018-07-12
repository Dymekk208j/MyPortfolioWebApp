using System;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Contact
    {
        public int ContactId{ get; set; }
        public String GitHubLink { get; set; }
        public String LinkedInLink { get; set; }
        public String FacebookLink { get; set; }
        public String Email1 { get; set; }
        public String Email2 { get; set; }
        public String PhoneNumber { get; set; }
    }
}