using LotusRMS.Models.Dto.TableDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ITableService
    {
        Task<Guid> Create(CreateTableDTO dto);
        Guid Update(UpdateTableDTO dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_Table> GetAll();
        public LotusRMS_Table GetByGuid(Guid Id);
    }
}
