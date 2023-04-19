using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Menu
{
    public class CreateMenuDetailVM
    {
        [Required]
        public double Quantity { get; set; }

        [Required]
        public double Rate{ get; set; }
        public bool IsDefault { get; set; }

    }
}
