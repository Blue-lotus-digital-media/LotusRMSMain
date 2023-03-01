using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.ProductDTO
{
    public class UpdateProductDTO:CreateProductDTO
    {
        public UpdateProductDTO(string product_Name, string product_Description, Guid product_Unit_Id, Guid product_Category_Id) : base(product_Name, product_Description, product_Unit_Id, product_Category_Id)
        {
        }

        public Guid Id { get; set; }

    }
}
