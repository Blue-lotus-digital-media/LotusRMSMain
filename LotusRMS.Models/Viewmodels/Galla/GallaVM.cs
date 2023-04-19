using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Galla
{
    public class GallaVM
    {
        public Guid GallaId { get; set; }
        public DateTime Date { get; set; }
        public double Opening_Balance { get; set; }
        public double Closing_Balance { get; set; }
        public ICollection<GallaDetailVM> Galla_Details { get; set; }

    }
}
