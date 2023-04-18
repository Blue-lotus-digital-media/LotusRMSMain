using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Galla
{
    public class CreateGallaVM
    {
        public DateTime? Date { get; set; }
        public string? User { get; set; }
        [Required(ErrorMessage ="Opening Balance required..")]
        [Range(1,10000,ErrorMessage ="Opening Banance Must be between 1 to 10000/-...")]
        public double Opening_Balance { get; set; }




    }
}
