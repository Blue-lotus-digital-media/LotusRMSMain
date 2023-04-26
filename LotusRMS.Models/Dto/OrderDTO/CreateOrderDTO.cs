using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.OrderDTO
{
    public class CreateOrderDTO
    {
        public Guid Table_Id { get; set; }
        public string UserId { get; set; }
        public List<CreateOrderDetailDTO> OrderDetails { get; set; }
        
    }
    public class CreateOrderDetailDTO
    {
        public Guid Menu_Id { get; set; }
        public double Quantity { get; set; }
        public  Guid  Quantity_Id { get; set; }
        public double Rate { get; set; }
        public string Remarks { get; set; }
    }
}
