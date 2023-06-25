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
    public class FiscalYearRepository : BaseRepository<LotusRMS_FiscalYear>, IFiscalYearRepository
    {
        private readonly ApplicationDbContext _dal;
        public FiscalYearRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public  async Task UpdateAsync(LotusRMS_FiscalYear obj)
        {
            var fiscalYear =await GetByGuidAsync(obj.Id).ConfigureAwait(false);
            fiscalYear.StartDateAD = obj.StartDateAD;
            fiscalYear.StartDateBS = obj.StartDateBS;
            fiscalYear.EndDateAD = obj.EndDateAD;
            fiscalYear.EndDateBS = obj.EndDateBS;


            var activeYear = await GetFirstOrDefaultAsync(x => x.IsActive).ConfigureAwait(false);
            if (activeYear.Id != obj.Id && obj.IsActive)
            {
                activeYear.IsActive = false;
                fiscalYear.IsActive = true;
            }
           await SaveAsync().ConfigureAwait(false);
        }

        public async Task UpdateActiveAsync(Guid Id)
        {
            var activeYear = await GetFirstOrDefaultAsync(x => x.IsActive).ConfigureAwait(false);
            if (activeYear != null)
            {
                activeYear.IsActive = false;
            }
            var currentYear = await GetByGuidAsync(Id).ConfigureAwait(false);
            currentYear.IsActive = true;
            await SaveAsync();
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<LotusRMS_FiscalYear?> GetActiveAsync()
        {
            return await GetQueryable().Where(a => a.IsActive).SingleOrDefaultAsync()
             .ConfigureAwait(false);
        } 
        public async Task<LotusRMS_FiscalYear?> GetByMyIdAsync(Guid Id)
        {
            return await GetQueryable().Where(a => a.Id==Id).SingleOrDefaultAsync()
             .ConfigureAwait(false);
        }
    }
}
