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
    
    public class TableTypeService : ITableTypeService
    {

        public readonly ITableTypeRepository _ITableTypeRepository;
        
        public TableTypeService(ITableTypeRepository iTableTypeRepository)
        {
            _ITableTypeRepository = iTableTypeRepository;
        }
        public async Task<Guid> CreateAsync(CreateTypeDTO dto)
        {
            var type = new LotusRMS_Table_Type(dto.Type_Name, dto.Type_Description);
            await _ITableTypeRepository.AddAsync(type).ConfigureAwait(false);

            return type.Id;
        }

      
        public async Task<IEnumerable<LotusRMS_Table_Type>> GetAllAvailableAsync()
        {
            return await _ITableTypeRepository.GetAllAsync(x=>!x.IsDelete&&x.Status).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LotusRMS_Table_Type>> GetAllAsync()
        {
            return await _ITableTypeRepository.GetAllAsync().ConfigureAwait(false);
        }
        public async Task<LotusRMS_Table_Type> GetFirstOrDefaultByIdAsync(Guid TypeId)
        {
            return await _ITableTypeRepository.GetFirstOrDefaultAsync(x=>x.Id==TypeId).ConfigureAwait(false);
        }

       public async Task<LotusRMS_Table_Type> GetByGuidAsync(Guid Id)
        {
            return await _ITableTypeRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<Guid> UpdateAsync(UpdateTypeDTO dto)
        {
            var type = await _ITableTypeRepository.GetByGuidAsync(dto.Id).ConfigureAwait(false) ?? throw new Exception();
            type.Update(dto.Type_Name, dto.Type_Description);
            await _ITableTypeRepository.UpdateAsync(type).ConfigureAwait(false);
            return type.Id;
        }

      

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            await _ITableTypeRepository.UpdateStatusAsync(Id);
            return Id;
        }

       public async Task<bool> IsDuplicateName(string Name, Guid Id = default)
        {
            var tableType = await _ITableTypeRepository.GetFirstOrDefaultAsync(x => x.Type_Name == Name).ConfigureAwait(false);
            if (tableType == null)
            {
                return false;
            }
            else
            {
                if(Id!=Guid.Empty && tableType.Id == Id)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
    }
}
