using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Unit_Name { get; private set; }
        public string Unit_Symbol { get; private set; }
        public string Unit_Description { get; private set; }
        public bool Status { get; private set; } 

        public LotusRMS_Unit(string unit_Name,string unit_Symbol,string unit_Description)
        {
            Unit_Name = unit_Name;
            Unit_Symbol = unit_Symbol;
            Unit_Description = unit_Description;
        }

        public void Update(string unit_Name, string unit_Symbol, string unit_Description,bool status)
        {
            Unit_Name = unit_Name;
            Unit_Symbol = unit_Symbol;
            Unit_Description = unit_Description;
            Status = status;
        }
    }
}
