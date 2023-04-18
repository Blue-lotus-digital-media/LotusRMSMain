using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_GallaDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public double Withdrawl { get; set; }
        public double Deposit { get; set; }
        public double Balance { get; set; }


    }
}
