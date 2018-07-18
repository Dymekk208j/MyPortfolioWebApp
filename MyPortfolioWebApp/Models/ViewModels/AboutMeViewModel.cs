using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class AboutMeViewModel
    {
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Musisz wprowadzić tytuł")]
        [StringLength(50, ErrorMessage = "Tytuł może mieć maksymalnie 50 znaków")]
        public string Title { get; set; }

        [Display(Name = "Treść")]
        [Required(ErrorMessage = "Musisz wprowadzić treść")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Link do obrazka")]
        public string ImageLink { get; set; }
    }
}