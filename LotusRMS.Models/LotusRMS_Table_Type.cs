using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Table_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string Type_Name { get; private set; }
        public string? Type_Description { get; private set; }
        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        protected LotusRMS_Table_Type() { }
        public LotusRMS_Table_Type(string type_Name, string type_Description)
        {
            Type_Name = type_Name;
            Type_Description = type_Description;
        }
        public void Update(string type_Name, string type_Description)
        {
            Type_Name = type_Name;
            Type_Description = type_Description;
        }
    }
}
