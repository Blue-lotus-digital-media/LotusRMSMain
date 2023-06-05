using LotusRMS.Utility;
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
        [Required(ErrorMessage ="Product Name Requied!")]
        public string Product_Name { get; set; }

        [Required(ErrorMessage ="Product Description Required!")]
        public string Product_Description { get; set; }

        [Required(ErrorMessage ="Product Unit must be selected")]
        
        public Guid Product_Unit_Id { get;  set; }
        public string? Product_Unit { get;  set; }

        [Required(ErrorMessage ="Product category must be selected")]
        public Guid Product_Category_Id { get;  set; }
        public string? Product_Category { get;  set; }
        [Required(ErrorMessage ="Product type must be selected") ]
        public Guid Product_Type_Id { get; set; }
        public string? Product_Type { get; set; }
        public double Unit_Quantity { get; set; }
        public CreateInventory Inventory { get; set; } = new CreateInventory();
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        public IEnumerable<SelectListItem>? UnitList { get; set; }
        public IEnumerable<SelectListItem>? TypeList { get; set; }
    }
}
