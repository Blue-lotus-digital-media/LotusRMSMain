using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CategoryDTO
{
    public class UpdateCategoryStatusDTO
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }
}
