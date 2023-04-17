using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Galla
{
    public class UpdateGallaDetailVM
    {
        public Guid GallaId { get; set; }
        public ICollection<CreateGallaDetailVM> GallaDetail { get; set; }

    }
}
