
using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{

    public class UnitRepository : BaseRepository<LotusRMS_Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _dal;
        public UnitRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

      

        public async Task UpdateAsync(LotusRMS_Unit lUnit)
        {
            var unit = await GetFirstOrDefaultAsync(s => s.Id ==  lUnit.Id).ConfigureAwait(false);
            if (unit != null)
            {
                unit.Update(lUnit.Unit_Name, lUnit.Unit_Symbol, lUnit.Unit_Description);

                await SaveAsync().ConfigureAwait(false);
            }
        }
        public async Task UpdateStatusAsync(Guid Id)
        {
            var unit = await GetFirstOrDefaultAsync(s => s.Id == Id).ConfigureAwait(false);
            if (unit != null)
            {
                unit.Status = !unit.Status;

                await SaveAsync().ConfigureAwait(false);
            }
        }
    }
}
