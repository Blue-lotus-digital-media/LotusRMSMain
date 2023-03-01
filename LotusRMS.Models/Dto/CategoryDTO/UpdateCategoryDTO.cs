using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CategoryDTO
{
    public class UpdateCategoryDTO:CreateCategoryDTO
    {
        public UpdateCategoryDTO(string category_Name, string category_Description) : base(category_Name, category_Description)
        {
        }

        public Guid Id { get; set; }
       
        
    }
}
