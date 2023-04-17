using LotusRMS.Models.Viewmodels.Galla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.GallaDTO
{
    public class CreateGallaDTO
    {
        public DateTime Date { get; set; }
        public string Cashier { get; set; }
        public ICollection<CreateGallaDetailVM> GallaDetail { get; set; }
    }
}
