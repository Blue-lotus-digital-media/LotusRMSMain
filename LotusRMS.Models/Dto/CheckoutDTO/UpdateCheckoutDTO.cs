using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CheckoutDTO
{
    public class UpdateCheckoutDTO:CreateCheckoutDTO
    {
        public Guid Id { get; set; }
    }
}
