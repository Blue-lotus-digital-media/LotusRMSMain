using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Category
{
    public class CreateCategoryVM
    {
        [Required]
        public string Category_Name { get; set; }

        public string? Category_Description { get; set; }
        [Required]
        public Guid Type_Id { get; set; }

        public IEnumerable<SelectListItem>? TypeList { get; set; } 

     
    }
}
