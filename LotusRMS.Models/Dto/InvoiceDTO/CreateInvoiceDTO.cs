using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.InvoiceDTO
{
    public class CreateInvoiceDTO
    {
       
        public Guid Checkout_Id { get; set; }
    }
      
}
