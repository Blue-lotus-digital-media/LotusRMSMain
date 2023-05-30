using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_MenuIncredians
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }
        [ForeignKey(nameof(Product_Id))]
        public LotusRMS_Product Product { get; set; }
        public double Quantity { get; set; }
        public Guid Unit_Id { get; set; }
        [ForeignKey(nameof(Unit_Id))]
        public LotusRMS_Unit? Unit { get; set; }


    }
}
