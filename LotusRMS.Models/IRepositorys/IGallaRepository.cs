using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IGallaRepository :IBaseRepository<LotusRMS_Galla>
    {
        Task AddNewDetailAsync(LotusRMS_Galla obj);
        Task<LotusRMS_Galla?> GetGallaTodayAsync(string UserId);
        Task<LotusRMS_Galla?> GetGallaByDateAsync(string Date,string UserId);
        Task<LotusRMS_Galla?> GetLastGallaAsync(string UserId);
        Task<LotusRMS_Galla?> GetGallaByGuidAsync(Guid Id);
        
    }
}
