using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Menu_Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Category_Name { get; private set; }
        public string Category_Description { get; private set; }
        public Guid Type_Id { get; private set; }
        [ForeignKey("Type_Id")]
        public LotusRMS_Menu_Type? Product_Type { get; set; }

        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        public LotusRMS_Menu_Category(string category_Name, string category_Description, Guid type_Id)
        {
            Category_Name = category_Name;
            Category_Description = category_Description;
            Type_Id = type_Id;
        }
        protected LotusRMS_Menu_Category() { }

        public void Update(string category_Name, string category_Description, Guid type_Id)
        {
            Category_Name = category_Name;
            Category_Description = category_Description;
            Type_Id = type_Id;

        }
    }
}
