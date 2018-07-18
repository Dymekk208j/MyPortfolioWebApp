
namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class PrivateInformation
    {
        public int PrivateInformationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string HomePage { get; set; }
        public string Email { get; set; }
        public string ImageLink { get; set; }
    }
}