using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.DueBook
{
    public class CreateDueBookVM
    {
        public string DueDate { get; set; }
        public Guid Invoice_Id { get; set; }
        public float DueAmount { get; set; }
        public float PaidAmount { get; set; }
    }
}
