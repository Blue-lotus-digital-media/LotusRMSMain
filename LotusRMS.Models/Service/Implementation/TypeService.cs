using LotusRMS.Models.Dto.TypeDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LotusRMS.Models.Service.Implementation
{

    public class TypeService : ITypeService
    {

        public readonly ITypeRepository _ITypeRepository;
        public TypeService(ITypeRepository iTypeRepository)
        {
            _ITypeRepository = iTypeRepository;
        }
        public async Task<Guid> CreateAsync(CreateTypeDTO dto)
        {
            var type = new LotusRMS_Product_Type(dto.Type_Name, dto.Type_Description);
            await _ITypeRepository.AddAsync(type).ConfigureAwait(false);
            await _ITypeRepository.SaveAsync().ConfigureAwait(false);
            return type.Id;
        }
       public async Task<IEnumerable<LotusRMS_Product_Type>> GetAllAsync()
        {
            return await _ITypeRepository.GetAllAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<LotusRMS_Product_Type>> GetAllAvailableAsync()
        {
            return await _ITypeRepository.GetAllAsync(x=>x.Status && !x.IsDelete).ConfigureAwait(false);
        }

        public async Task<LotusRMS_Product_Type> GetByGuidAsync(Guid Id)
        {
            return await _ITypeRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<bool> IsDuplicateName(string Name, Guid Id = new Guid())
        {
            var type = await _ITypeRepository.GetFirstOrDefaultAsync(x => x.Type_Name == Name).ConfigureAwait(false);
            if (type == null)
            {
                return false;
            }
            else
            {
                if(Id!=Guid.Empty && type.Id == Id)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<Guid> UpdateAsync(UpdateTypeDTO dto)
        {
            var type = await _ITypeRepository.GetByGuidAsync(dto.Id).ConfigureAwait(false) ?? throw new Exception();
            type.Update(dto.Type_Name, dto.Type_Description);
            await _ITypeRepository.UpdateAsync(type).ConfigureAwait(false);
            await _ITypeRepository.SaveAsync().ConfigureAwait(false);
            return type.Id;
        }
        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            await _ITypeRepository.UpdateStatusAsync(Id).ConfigureAwait(false);
            return Id;
        }
    }
}
