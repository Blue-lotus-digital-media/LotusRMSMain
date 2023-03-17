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

        public bool CheckActive()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Create(CreateBillSettingDTO dto)
        {
            var activeBill = GetActive();
            

            var bs = new LotusRMS_BillSetting()
            {
                BillPrefix = dto.BillPrefix,
                BillAddress = dto.BillAddress,
                BillTitle = dto.BillTitle,
                BillNote = dto.BillNote,
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
          
            _billSettingRepository.Add(bs);
            _billSettingRepository.Save();
            return Task.FromResult(bs.Id);
        }

        public LotusRMS_BillSetting GetActive()
        {
            return _billSettingRepository.GetFirstOrDefault(x => x.IsActive);
        }

        public IEnumerable<LotusRMS_BillSetting> GetAll()
        {
            return _billSettingRepository.GetAll();
        }

        public IEnumerable<LotusRMS_BillSetting> GetAllAvailable()
        {
            return _billSettingRepository.GetAll(x => !x.IsDelete);
        }

        public LotusRMS_BillSetting GetByGuid(Guid Id)
        {
            return _billSettingRepository.GetByGuid(Id);
        }

        public Guid Update(UpdateBillSettingDTO dto)
        {
            var billsetting = new LotusRMS_BillSetting()
            {
                Id = dto.Id,
                BillPrefix = dto.BillPrefix,
                BillAddress = dto.BillAddress,
                BillTitle = dto.BillTitle,
                BillNote = dto.BillNote,
                IsPanOrVat = dto.IsPanOrVat,
                IsPhone = dto.IsPhone,
                IsActive = dto.IsActive,
            };
            _billSettingRepository.Update(billsetting);
            return dto.Id;
        }

        public Guid UpdateActive(Guid Id)
        {
            _billSettingRepository.UpdateActive(Id);

            return Id;
        }
    }
}
