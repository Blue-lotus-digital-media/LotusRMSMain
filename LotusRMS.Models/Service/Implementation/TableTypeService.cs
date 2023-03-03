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

     

        public Guid Create(CreateTypeDTO dto)
        {
            var type=new LotusRMS_Table_Type(dto.Type_Name,dto.Type_Description);
            _ITableTypeRepository.Add(type);
            
            return type.Id;

        }

        public async Task<Guid> CreateAsync(CreateTypeDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LotusRMS_Table_Type> GetAll()
        {
            return _ITableTypeRepository.GetAll();
        }

        public async Task<IEnumerable<LotusRMS_Table_Type>> GetAllAsync()
        {
            return await _ITableTypeRepository.GetAllAsync();
        }

        public LotusRMS_Table_Type GetByGuid(Guid Id)
        {
            return _ITableTypeRepository.GetByGuid(Id);
        }

        public async Task<LotusRMS_Table_Type> GetByGuidAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Guid Update(UpdateTypeDTO dto)
        {
            //using var tx = new TransactionScope();
            var type = _ITableTypeRepository.GetByGuid(dto.Id) ?? throw new Exception();
            type.Update(dto.Type_Name, dto.Type_Description);

            _ITableTypeRepository.Update(type);
            //todo logic

           // tx.Complete();
            return type.Id;
        }

        public async Task<Guid> UpdateAsync(UpdateTypeDTO dto)
        {
            throw new NotImplementedException();
        }

        public Guid UpdateStatus(Guid Id)
        {
            _ITableTypeRepository.UpdateStatus(Id);
            return Id;
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

       
    }
}
