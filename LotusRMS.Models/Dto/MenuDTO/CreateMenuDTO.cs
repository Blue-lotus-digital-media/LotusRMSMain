using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.MenuDTO
{
    public class CreateMenuDTO
    {
        public string Item_name { get; set; }

        public Guid Unit_Id { get; set; }


        public Guid Type_Id { get; set; }
        public Guid Category_Id { get; set; }
        public string OrderTo { get; set; }
        public Byte[] Image { get; set; }
        public ICollection<CreateMenuDetailDTO> Menu_Details { get; set; }

    }
    public class CreateMenuDetailDTO
    {
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public bool Default { get; set; }
    }
}
