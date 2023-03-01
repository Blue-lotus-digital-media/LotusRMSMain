using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.ProductDTO
{
    public class CreateProductDTO
    {
        public string Product_Name { get; set; }
        public string Product_Description { get; set; }
        public Guid Product_Unit_Id { get; set; }
        public Guid Product_Category_Id { get; set; }   

        public CreateProductDTO(string product_Name, string product_Description, Guid product_Unit_Id, Guid product_Category_Id)
        {
            Product_Name = product_Name;
            Product_Description = product_Description;
            Product_Unit_Id = product_Unit_Id;
            Product_Category_Id = product_Category_Id;
        }   
    }
}
