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

        public string Category_Description { get; set; }

        public bool Status { get; set; }
        public bool IsDelete { get; set; }
    }
}
