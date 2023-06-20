using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class TypeRepository : BaseRepository<LotusRMS_Product_Type>, ITypeRepository
    {
        public readonly ApplicationDbContext _dal;
        public TypeRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task UpdateAsync(LotusRMS_Product_Type lType)
        {
            var type = await GetFirstOrDefaultAsync(x=>x.Id== lType.Id).ConfigureAwait(false);
            if (type != null)
            {
                type.Update(lType.Type_Name, lType.Type_Description);
               await SaveAsync();
            }
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            var type = await GetFirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
            if (type != null)
            {
                type.Status = !type.Status;
                await SaveAsync();
            }
        }
    }
}
