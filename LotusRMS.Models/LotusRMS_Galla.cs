using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Galla
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Cashier { get; set; }
        [ForeignKey("Cashier")]
        public virtual RMSUser User { get; set; }
        public ICollection<LotusRMS_GallaDetail> Galla_Details { get; set; }
        public double Opening_Balance { get; set; }
        public double Closing_Balance { get; set; }

    }
}
