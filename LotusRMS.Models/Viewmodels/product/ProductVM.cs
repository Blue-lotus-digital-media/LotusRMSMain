

using LotusRMS.Utility;
using System.ComponentModel.DataAnnotations;

namespace LotusRMS.Models.Viewmodels.product
{
    public class ProductVM
    {

        public Guid Id { get; set; }
        [Required(ErrorMessage ="Product Name Required")]
        public string Product_Name { get; set; }


        [Required(ErrorMessage = "Product Description Required")]
        public string Product_Description { get; set; }


        [Required(ErrorMessage = "Please Select Product Unit")]
        public Guid Product_Unit_Id { get; set; }
        public string? Product_Unit { get; set; }


        [Required(ErrorMessage = "Please Select Product Category")]
        public Guid Product_Category_Id { get; set; }
        public string? Product_Category { get; set; }



        [Required(ErrorMessage = "Please Select Product Type")]
        public Guid Product_Type_Id { get; set; }
        public string? Product_Type { get; set; }
        public float Unit_Quantity { get; set; }

        public float? Stock_Quantity { get; set; }
        public bool Status { get; set; }



        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        public IEnumerable<SelectListItem>? UnitList { get; set; }
        public IEnumerable<SelectListItem>? TypeList { get; set; }

    }
}
