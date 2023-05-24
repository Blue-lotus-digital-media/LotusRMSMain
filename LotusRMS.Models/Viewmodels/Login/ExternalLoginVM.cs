using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Login
{
    public class ExternalLoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ClaimsPrincipal? Principal { get; set; }
    }
}
