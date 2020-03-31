using System.ComponentModel.DataAnnotations;

namespace BeanApp.Domain.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public LoginUser() { }
    }
}
