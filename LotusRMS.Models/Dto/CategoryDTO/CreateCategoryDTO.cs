using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CategoryDTO
{
    public class CreateCategoryDTO
    {
        public string Category_Name { get; set; }
        public string Category_Description { get; set; }

        public CreateCategoryDTO(string category_Name, string category_Description)
        {
            Category_Name = category_Name;
            Category_Description = category_Description;
        }
    }
}
