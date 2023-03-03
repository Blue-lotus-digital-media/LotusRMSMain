using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ITableTypeRepository : IBaseRepository<LotusRMS_Table_Type>
    {
        void Update(LotusRMS_Table_Type type);
        void UpdateStatus(Guid Id);
    }
}
