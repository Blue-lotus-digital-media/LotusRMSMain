using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ITableRepository : IBaseRepository<LotusRMS_Table>
    {
        void Update(LotusRMS_Table table);
        void UpdateStatus(Guid Id);
        bool UpdateReserved(Guid Id);
    
}
}
