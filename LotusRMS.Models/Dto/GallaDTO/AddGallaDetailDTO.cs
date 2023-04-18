using LotusRMS.Models.Viewmodels.Galla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.GallaDTO
{
    public class AddGallaDetailDTO
    {
        public Guid Galla_Id { get; set; }
        public double Closing_Balance { get; set; }
        public CreateGallaDetailVM GallaDetail { get; set; }
    }
}
