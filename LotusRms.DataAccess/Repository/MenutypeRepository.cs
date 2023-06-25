using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class MenuTypeRepository : BaseRepository<LotusRMS_Menu_Type>, IMenuTypeRepository
    {
        public readonly ApplicationDbContext _dal;
        public MenuTypeRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task UpdateAsync(LotusRMS_Menu_Type lType)
        {
            var type = await GetFirstOrDefaultAsync(x => x.Id == lType.Id).ConfigureAwait(false);
            if (type != null)
            {
                type.Update(type.Type_Name, type.Type_Description);
                await SaveAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            var type = await GetFirstOrDefaultAsync(x => x.Id == Id);
            if (type != null)
            {
                type.Status = !type.Status;
                await SaveAsync().ConfigureAwait(false);
            }
        }
    }
}
