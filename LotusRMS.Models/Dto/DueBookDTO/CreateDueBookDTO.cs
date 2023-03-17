using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.DueBookDTO
{
    public class CreateDueBookDTO
    {
        public string DueDate { get; set; }
        public Guid Invoice_Id { get; set; }
        public float DueAmount { get; set; }
        public float PaidAmount { get; set; }
    }
}
