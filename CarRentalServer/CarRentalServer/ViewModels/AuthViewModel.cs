using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }
    }
}
