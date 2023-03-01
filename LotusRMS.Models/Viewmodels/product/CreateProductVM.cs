using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.product
{
    public class CreateProductVM
    {
        [Required]
        public string Product_Name { get; set; }

        [Required]
        public string Product_Description { get; set; }

        [Required]
        public Guid Product_Unit_Id { get;  set; }
        public LotusRMS_Unit? Product_Unit { get;  set; }

        [Required]
        public Guid Product_Category_Id { get;  set; }
        public LotusRMS_Product_Category? Product_Category { get;  set; }

        

       

    }
}
