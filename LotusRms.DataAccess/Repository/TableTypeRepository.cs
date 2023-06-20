using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class TableTypeRepository : BaseRepository<LotusRMS_Table_Type>, ITableTypeRepository
    {
        public readonly ApplicationDbContext _dal;
        public TableTypeRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }
        public async Task UpdateAsync(LotusRMS_Table_Type lType)
        {
            var type =await GetFirstOrDefaultAsync(x => x.Id == lType.Id).ConfigureAwait(false);
            if (type != null)
            {
                type.Update(lType.Type_Name, lType.Type_Description);
                await SaveAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            var type = await GetFirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
            if (type != null)
            {
                type.Status = !type.Status;
                await SaveAsync().ConfigureAwait(false);
            }
        }
    }
}
