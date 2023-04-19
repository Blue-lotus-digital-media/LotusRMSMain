using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Galla
{
    public class GallaDetailVM
    {
        public Guid Detail_Id { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public double Withdrawl { get; set; }
        public double Deposit { get; set; }
        public double Balance { get; set; }
    }
}
