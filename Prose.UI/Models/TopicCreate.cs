using System.ComponentModel.DataAnnotations;

namespace Prose.UI.Models
{
    public class TopicCreate
    {

        [Required(ErrorMessage = "Title is required", AllowEmptyStrings = false)]
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text is required", AllowEmptyStrings = false)]
        [Display(Name = "Text")]
        public string Text { get; set; }
       
        [Display(Name = "Keyword")]
        public string Keyword { get; set; }
        public int TopicId { get; set; }
    }
}