using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required, RegularExpression(@"^[STFG]\d{7}[A-Z]$", ErrorMessage = "Invalid NRIC."), MaxLength(9)]
        public string NRIC { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string BirthDate { get; set; }

        public string? Resume { get; set; } = string.Empty;

        [Required]
        public string? WhoamI { get; set; } = string.Empty;


    }
}
