using LotusRMS.Models.Dto.BillSettingDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class BillSettingService : IBillSettingService
    {
        private readonly IBillSettingRepository _billSettingRepository;

        public BillSettingService(IBillSettingRepository billSettingRepository)
        {
            _billSettingRepository = billSettingRepository;
        }

        public Task<bool> CheckActiveActive()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateAsync(CreateBillSettingDTO dto)
        {
            var activeBill = await GetActiveAsync().ConfigureAwait(false);
            var bs = new LotusRMS_BillSetting()
            {
                BillPrefix = dto.BillPrefix,
                BillAddress = dto.BillAddress,
                BillTitle = dto.BillTitle,
                BillNote = dto.BillNote,
                IsFiscalYear=dto.IsFiscalYear,
                IsPanOrVat = dto.IsPanOrVat,
                IsPhone = dto.IsPhone,
                IsActive=dto.IsActive,
                

            }; 
            if (activeBill == null) {
                bs.IsActive = true;
            }
            else if(dto.IsActive)
            {
                activeBill.IsActive = false;

            }
          
          await _billSettingRepository.AddAsync(bs).ConfigureAwait(false);
            await _billSettingRepository.SaveAsync().ConfigureAwait(false);
            return bs.Id;
        }

        public async Task<LotusRMS_BillSetting> GetActiveAsync()
        {
            return await _billSettingRepository.GetFirstOrDefaultAsync(x => x.IsActive).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LotusRMS_BillSetting>> GetAllAsync()
        {
            return await _billSettingRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<LotusRMS_BillSetting>> GetAllAvailableAsync()
        {
            return await _billSettingRepository.GetAllAsync(x => !x.IsDelete).ConfigureAwait(false);
        }

        public async Task<LotusRMS_BillSetting> GetByGuidAsync(Guid Id)
        {
            return await _billSettingRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<Guid> UpdateAsync(UpdateBillSettingDTO dto)
        {
            var billsetting = new LotusRMS_BillSetting()
            {
                Id = dto.Id,
                BillPrefix = dto.BillPrefix,
                BillAddress = dto.BillAddress,
                BillTitle = dto.BillTitle,
                BillNote = dto.BillNote,
                IsFiscalYear=dto.IsFiscalYear,
                IsPanOrVat = dto.IsPanOrVat,
                IsPhone = dto.IsPhone,
                IsActive = dto.IsActive,
            };
            await _billSettingRepository.UpdateAsync(billsetting).ConfigureAwait(false);
            return dto.Id;
        }

        public async Task<Guid> UpdateActiveAsync(Guid Id)
        {
            await _billSettingRepository.UpdateActiveAsync(Id).ConfigureAwait(false);

            return Id;
        }

        public Task<bool> CheckActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDuplicateName(string Name, Guid Id = default)
        {
            throw new NotImplementedException();
        }
    }
}
