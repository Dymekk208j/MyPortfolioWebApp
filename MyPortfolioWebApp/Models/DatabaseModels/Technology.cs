

namespace MyPortfolioWebApp.Models.DatabaseModels
{
    public class Technology
    {
        public int TechnologyId { get; set; }
        public int LevelOfKnowledge { get; set;} // 0 - very well, 1 - well, 2 - ok
        public string Name { get; set; }
        public bool ShowInCv { get; set; }

    }
}