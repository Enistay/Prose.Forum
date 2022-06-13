using System.ComponentModel.DataAnnotations;

namespace Prose.UI.Models
{
    public class UserSignup
    {
        [Required(ErrorMessage = "Enter Username", AllowEmptyStrings=false)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter Password between 4 and 50 caracters", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}