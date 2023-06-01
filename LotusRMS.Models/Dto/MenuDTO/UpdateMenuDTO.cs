using LotusRMS.Models.Viewmodels.Menu;
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
        public ICollection<UpdateMenuDetailDTO> UpdateMenuDetail { get; set; } = new List<UpdateMenuDetailDTO>();
        public ICollection<UpdateMenuIncredianDTO> UpdateMenuIncredian { get; set; } = new List<UpdateMenuIncredianDTO>();

    }
    public class UpdateMenuDetailDTO:CreateMenuDetailDTO {
        public Guid Id { get; set; }
        public bool? IsDelete { get; set; }
    }
    public class UpdateMenuIncredianDTO:CreateMenuIncredianDTO {
        public Guid Id { get; set; }
        public bool? IsDelete { get; set; }
    }
}
