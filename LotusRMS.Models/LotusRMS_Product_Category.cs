using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Product_Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Category_Name { get; private set; }
        public string Category_Description { get; private set; }

        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        public LotusRMS_Product_Category(string category_Name,string category_Description)
        {
            Category_Name = category_Name;
            Category_Description = category_Description;
        }
        protected LotusRMS_Product_Category() { }

        public void Update(string category_Name, string category_Description)
        {
            Category_Name = category_Name;
            Category_Description = category_Description;
         
        }

    }

   
}
