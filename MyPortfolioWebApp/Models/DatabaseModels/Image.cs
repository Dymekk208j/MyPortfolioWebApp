namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Image
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public int ProjectId { get; set; }
        public bool TempraryProject { get; set; }
    }
}