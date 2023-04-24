using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Menu_Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Unit_Name { get; private set; }
        public string Unit_Symbol { get; private set; }
        public string Unit_Description { get; private set; }
        public List<LotusRMS_Unit_Division> UnitDivision { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        public LotusRMS_Menu_Unit(string unit_Name, string unit_Symbol, string unit_Description)
        {
            Unit_Name = unit_Name;
            Unit_Symbol = unit_Symbol;
            Unit_Description = unit_Description;

        }



        protected LotusRMS_Menu_Unit()
        {
        }

        public void Update(string unit_Name, string unit_Symbol, string unit_Description)
        {
            Unit_Name = unit_Name;
            Unit_Symbol = unit_Symbol;
            Unit_Description = unit_Description;
        }
    }
    public class LotusRMS_Unit_Division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Value { get; set; }

    }
}
