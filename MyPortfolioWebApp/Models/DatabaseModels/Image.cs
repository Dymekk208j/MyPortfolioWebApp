using Microsoft.Owin.Security.Provider;

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Image
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }

        public int ProjectId { get; set; }
        public bool TempraryProject { get; set; }

        public string GetImageLink()
        {
            string link = TempraryProject
                ? @"https://damiandziuraportfolio.blob.core.windows.net/tempprojectimages/"
                : "https://damiandziuraportfolio.blob.core.windows.net/projectimages/";

            return link + FileName;
        }


        // => FileName = TempraryProject ? "TempProject" + ProjectId + value : "Project" + ProjectId + value;
    }
}