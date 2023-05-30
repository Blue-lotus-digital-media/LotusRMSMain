using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Item_Name { get;private set; }
        public Guid Unit_Id { get; private set; }
        [ForeignKey("Unit_Id")] 
        public LotusRMS_Menu_Unit Menu_Unit { get; set; }

        public ICollection<LotusRMS_MenuDetail> Menu_Details { get; set; }

        public Guid Type_Id { get; private set; }

        [ForeignKey("Type_Id")]
        public LotusRMS_Menu_Type Menu_Type { get; set; }
        public Guid Category_Id { get; private set; }
        [ForeignKey("Category_Id")]
        public LotusRMS_Menu_Category Menu_Category { get; set; }

        public Byte[]? Image { get; set; }

        public string OrderTo { get; private set; }
        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public ICollection<LotusRMS_MenuIncredians> Menu_Incredians { get; set; } = new List<LotusRMS_MenuIncredians>();
        public void Update(string item_name,Guid unit_Id,Guid type_Id,Guid category_Id,string orderTo)
        {
            Item_Name = item_name;
            Type_Id = type_Id;
            Category_Id = category_Id;
            Unit_Id = unit_Id;
            OrderTo = orderTo;
        }
    }
}
