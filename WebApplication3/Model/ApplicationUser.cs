using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
	public class ApplicationUser : IdentityUser
	{
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "NRIC")]
        public string NRIC { get; set; }

        [Display(Name = "Date of Birth")]
        public string BirthDate { get; set; }

        public string? Resume { get; set; }

        [DataType(DataType.MultilineText)]
        public string WhoamI { get; set; }
    }
}
