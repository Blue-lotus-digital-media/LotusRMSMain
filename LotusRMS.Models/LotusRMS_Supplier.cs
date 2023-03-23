using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string? Contact1 { get; set; }
        public string? PanOrVat { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; }

        public void Update(string fullName, string address, string contact)
        {
            FullName = fullName;
            Address = address;
            Contact = contact;

        }

    }
}
