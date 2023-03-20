using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string? PanOrVat { get; set; }
        public List<LotusRMS_DueBook> DueBooks { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; }


        public void Update(string name,string address,string contact) {
            Name = name;
            Address = address;
            Contact = contact;
        }
    }
    public class LotusRMS_DueBook
    {
        public Guid Id { get; set; }
        public string DueDate { get; set; }
        public Guid? Invoice_Id { get; set; }
        [ForeignKey("Invoice_Id")]
        public virtual LotusRMS_Invoice Invoice { get; set; }
        public float Invoice_Amount { get; set; }
        public float DueAmount { get; set; }
        public float PaidAmount { get; set; }
        public float BalanceDue { get; set; }


    }
}
