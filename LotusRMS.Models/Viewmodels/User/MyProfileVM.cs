using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.User
{
    public class MyProfileVM
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Phone]
        [Display(Name = "Contact")]
        public string Contact { get; set; }
        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }
    }
}
