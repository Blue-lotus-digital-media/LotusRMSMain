using LotusRMS.Models.Dto.TypeDTO;
using LotusRMS.Models.Helper;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class MenuTypeService : IMenuTypeService
    {
        public readonly IMenuTypeRepository _IMenuTypeRepository;
        public MenuTypeService(IMenuTypeRepository iMenuTypeRepository)
        {
            _IMenuTypeRepository = iMenuTypeRepository;
        }



     

        public async Task<Guid> CreateAsync(CreateTypeDTO dto)
        {
            var type = new LotusRMS_Menu_Type(dto.Type_Name, dto.Type_Description);
            await _IMenuTypeRepository.AddAsync(type).ConfigureAwait(false);
            await _IMenuTypeRepository.SaveAsync().ConfigureAwait(false);
            return type.Id;
        }

       

        public async Task<IEnumerable<LotusRMS_Menu_Type>> GetAllAsync()
        {
            return await _IMenuTypeRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<LotusRMS_Menu_Type>> GetAllAvailableAsync()
        {
            return await _IMenuTypeRepository.GetAllAsync(x=>!x.IsDelete&&x.Status).ConfigureAwait(false);
        }

        public async Task<LotusRMS_Menu_Type?> GetByGuidAsync(Guid Id)
        {
            return await _IMenuTypeRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<Guid> UpdateAsync(UpdateTypeDTO dto)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var type = _IMenuTypeRepository.GetByGuid(dto.Id) ?? throw new Exception();
            type.Update(dto.Type_Name, dto.Type_Description);

           await _IMenuTypeRepository.UpdateAsync(type).ConfigureAwait(false);
            //todo logic

            scope.Complete();
            return type.Id;
        }


        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            await _IMenuTypeRepository.UpdateStatusAsync(Id).ConfigureAwait(false);
            scope.Complete();
            return Id;
        }


    }
}
