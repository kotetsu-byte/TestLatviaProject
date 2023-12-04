using System.ComponentModel.DataAnnotations;

namespace TestLatviaProject.View_Models
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remeber Me?")]
        public bool RememberMe { get; set; }
    }
}
