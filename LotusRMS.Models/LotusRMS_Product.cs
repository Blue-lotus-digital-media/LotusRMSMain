using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{

    public class LotusRMS_Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Product_Name { get; private set; }
        public string Product_Description { get; private set; }
        public Guid Product_Unit_Id { get; private set; }
        [ForeignKey(nameof(Product_Unit_Id))]
        public LotusRMS_Unit Product_Unit { get; set; }
        public Guid Product_Category_Id { get; private set; }
        [ForeignKey(nameof(Product_Category_Id))]
        public LotusRMS_Product_Category Product_Category { get; set; }

        public bool Status { get; set; } = true;
        public bool IsDelete { get; private set; } = false;




        public LotusRMS_Product(string product_Name,string product_Description,Guid product_Unit_Id, Guid product_Category_Id)
        {
            Product_Name = product_Name;
            Product_Description = product_Description;
            Product_Unit_Id = product_Unit_Id;
            Product_Category_Id = product_Category_Id;
           
                
        }    public void Update(string product_Name,string product_Description,Guid product_Unit_Id, Guid product_Category_Id)
        {
            Product_Name = product_Name;
            Product_Description = product_Description;
            Product_Unit_Id = product_Unit_Id;
            Product_Category_Id = product_Category_Id;
           
                
        }

    }
}
