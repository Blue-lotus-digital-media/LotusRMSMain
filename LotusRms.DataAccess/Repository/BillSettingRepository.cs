using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.FiscalYear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class BillSettingRepository : BaseRepository<LotusRMS_BillSetting>, IBillSettingRepository
    {
        private readonly ApplicationDbContext _dal;
        public BillSettingRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task UpdateAsync(LotusRMS_BillSetting obj)
        {
            var billSetting = GetByGuid(obj.Id);
            billSetting.BillPrefix = obj.BillPrefix;
            billSetting.IsPanOrVat = obj.IsPanOrVat;
            billSetting.IsPhone = obj.IsPhone;
            billSetting.BillAddress = obj.BillAddress;
            billSetting.BillNote = obj.BillNote;
            billSetting.BillTitle = obj.BillTitle;

            var activeSetting = await GetFirstOrDefaultAsync(x => x.IsActive).ConfigureAwait(false);
            if (activeSetting.Id != obj.Id && obj.IsActive)
            {
                activeSetting.IsActive = false;
                billSetting.IsActive = true;
            }
            await SaveAsync().ConfigureAwait(false);
        }

        public async Task UpdateActiveAsync(Guid Id)
        {
            var activeSetting = await GetFirstOrDefaultAsync(x => x.IsActive).ConfigureAwait(false);
            if (activeSetting != null)
            {
                activeSetting.IsActive = false;
            }
            var currentSetting = await GetByGuidAsync(Id).ConfigureAwait(false);
            currentSetting.IsActive = true;
            await SaveAsync().ConfigureAwait(false);
        }
        public Task UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
