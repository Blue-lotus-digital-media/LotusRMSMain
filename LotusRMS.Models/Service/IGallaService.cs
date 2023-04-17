using LotusRMS.Models.Dto.GallaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IGallaService
    {
        void CreateGalla(CreateGallaDTO dto);
        void AddGallaDetail();
        void UpdateGallaDetail();   


    }
}
