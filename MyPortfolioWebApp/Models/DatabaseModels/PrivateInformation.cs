using System;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class PrivateInformation
    {
        public int PrivateInformationId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String PostCode { get; set; }
        public String HouseNumber { get; set; }
        public String FlatNumber { get; set; }
        public String PhoneNumber { get; set; }
        public String HomePage { get; set; }
        public String Email { get; set; }
        public String ImageLink { get; set; }
    }
}