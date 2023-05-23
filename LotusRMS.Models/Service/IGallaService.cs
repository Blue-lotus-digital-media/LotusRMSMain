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
        void AddGallaDetail(AddGallaDetailDTO dto);
        void UpdateGallaDetail();
        LotusRMS_Galla GetTodayGalla();
        LotusRMS_Galla GetGallaByDate(string date,string UserId);
        LotusRMS_Galla GetLastGalla(string UserId);
        Task<double> GetGallaAmountAsync(); 


    }
}
