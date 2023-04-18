using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IGallaRepository :IBaseRepository<LotusRMS_Galla>
    {
        void AddNewDetail(LotusRMS_Galla obj);
        LotusRMS_Galla GetGallaToday(string UserId);
        LotusRMS_Galla GetGallaByDate(string Date,string UserId);
        LotusRMS_Galla GetGallaByGuid(Guid Id);
        
    }
}
