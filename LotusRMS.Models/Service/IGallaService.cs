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
        Task CreateGallaAsync(CreateGallaDTO dto);
        Task AddGallaDetailAsync(AddGallaDetailDTO dto);
        Task UpdateGallaDetailAsync();
        Task<LotusRMS_Galla?> GetTodayGallaAsync();
        Task<LotusRMS_Galla?> GetGallaByDateAsync(string date,string UserId);
        Task<LotusRMS_Galla?> GetLastGallaAsync(string UserId);
        Task<double> GetGallaAmountAsync(); 


    }
}
