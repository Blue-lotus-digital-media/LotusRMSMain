using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels
{
    public class ForgetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email{get;set;}
    }
}
