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

        public void Update(LotusRMS_BillSetting obj)
        {
            var billSetting = GetByGuid(obj.Id);
            billSetting.BillPrefix = obj.BillPrefix;
            billSetting.IsPanOrVat = obj.IsPanOrVat;
            billSetting.IsPhone = obj.IsPhone;
            billSetting.BillAddress = obj.BillAddress;
            billSetting.BillNote = obj.BillNote;
            billSetting.BillTitle = obj.BillTitle;

            var activeSetting = GetFirstOrDefault(x => x.IsActive);
            if (activeSetting.Id != obj.Id && obj.IsActive)
            {
                activeSetting.IsActive = false;
                billSetting.IsActive = true;
            }

            Save();

        }

        public void UpdateActive(Guid Id)
        {
            var activeSetting = GetFirstOrDefault(x => x.IsActive);
            if (activeSetting != null)
            {
                activeSetting.IsActive = false;
            }
            var currentSetting = GetByGuid(Id);
            currentSetting.IsActive = true;
            Save();
        }

        public void UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
