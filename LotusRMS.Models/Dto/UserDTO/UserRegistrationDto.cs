using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.UserDTO
{

    public class UserRegistrationDto
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }
        [Required]

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "User Role")]
        public string UserRole { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
       /* [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }*/

        [Display(Name = "Profile Picture")]
        public byte[]? ProfilePicture { get; set; }

        public string? ReturnUrl { get; set; }
    }

}
