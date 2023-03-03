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

     

        public Guid Create(CreateTypeDTO dto)
        {
            var type=new LotusRMS_Product_Type(dto.Type_Name,dto.Type_Description);
            _ITypeRepository.Add(type);
            _ITypeRepository.Save();
            return type.Id;

        }

        public async Task<Guid> CreateAsync(CreateTypeDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LotusRMS_Product_Type> GetAll()
        {
            return _ITypeRepository.GetAll();
        }

        public async Task<IEnumerable<LotusRMS_Product_Type>> GetAllAsync()
        {
            return await _ITypeRepository.GetAllAsync();
        }

        public LotusRMS_Product_Type GetByGuid(Guid Id)
        {
            return _ITypeRepository.GetByGuid(Id);
        }

        public async Task<LotusRMS_Product_Type> GetByGuidAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Guid Update(UpdateTypeDTO dto)
        {
            //using var tx = new TransactionScope();
            var type = _ITypeRepository.GetByGuid(dto.Id) ?? throw new Exception();
            type.Update(dto.Type_Name, dto.Type_Description);
           
            _ITypeRepository.Update(type);
            _ITypeRepository.Save();
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
            _ITypeRepository.UpdateStatus(Id);

            _ITypeRepository.Save();
            return Id;
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

       
    }
}
