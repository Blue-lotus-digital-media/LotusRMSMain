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
        public int Product_Unit_Id { get; private set; }
        [ForeignKey("Product_Unit_Id")]
        public LotusRMS_Unit Product_Unit { get; private set; }



    }
}
