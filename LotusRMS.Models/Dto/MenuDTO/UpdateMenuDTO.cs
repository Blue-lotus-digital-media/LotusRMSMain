using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.MenuDTO
{
    public class UpdateMenuDTO:CreateMenuDTO
    {
        public Guid Id { get; set; }
    }
}
