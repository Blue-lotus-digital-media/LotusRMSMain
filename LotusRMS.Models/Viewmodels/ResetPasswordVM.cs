using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels
{
    public class ResetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Password and confirm password doesn't matched")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; } 
    }
}
