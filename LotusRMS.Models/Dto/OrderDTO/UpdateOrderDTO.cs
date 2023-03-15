using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.OrderDTO
{
    public class UpdateOrderDTO
    {
        public Guid Order_Id { get; set; }
        public List<CreateOrderDetailDTO> OrderDetail { get; set; }
    }
}
