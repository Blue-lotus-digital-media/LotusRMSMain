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

        public float Rate { get; set; }
        public Guid Unit_Id { get; set; }

        public float Unit_Quantity { get; set; }

        public Guid Type_Id { get; set; }
        public Guid Category_Id { get; set; }
        public string OrderTo { get; set; }
        public Byte[] Image { get; set; }

    }
}
