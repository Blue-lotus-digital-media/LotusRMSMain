using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.TypeDTO
{
    public class UpdateTypeDTO : CreateTypeDTO
    {
        public UpdateTypeDTO(string type_Name, string type_Description) : base(type_Name, type_Description)
        {}
        public Guid Id { get; set; }
    }
}
