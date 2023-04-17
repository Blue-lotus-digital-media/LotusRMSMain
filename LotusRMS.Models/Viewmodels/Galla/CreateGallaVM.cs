using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Galla
{
    public class CreateGallaVM
    {
        public DateTime Date { get; set; }
        public ICollection<CreateGallaDetailVM> GallaDetail { get; set; }

      
    }
}
