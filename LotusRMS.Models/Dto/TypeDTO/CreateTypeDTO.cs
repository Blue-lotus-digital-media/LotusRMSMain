using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.TypeDTO
{
    public class CreateTypeDTO
    {
        public string Type_Name { get; set; }
        public string Type_Description { get; set; }    
        public CreateTypeDTO(string type_Name, string type_Description)
        {
            Type_Name = type_Name;
            Type_Description = type_Description;
        }   
    }
}
