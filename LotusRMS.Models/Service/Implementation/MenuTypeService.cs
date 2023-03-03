using LotusRMS.Models.Dto.TypeDTO;
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



        public Guid Create(CreateTypeDTO dto)
        {
            var type = new LotusRMS_Menu_Type(dto.Type_Name, dto.Type_Description);
            _IMenuTypeRepository.Add(type);
            _IMenuTypeRepository.Save();
            return type.Id;

        }

        public async Task<Guid> CreateAsync(CreateTypeDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LotusRMS_Menu_Type> GetAll()
        {
            return _IMenuTypeRepository.GetAll();
        }

        public async Task<IEnumerable<LotusRMS_Menu_Type>> GetAllAsync()
        {
            return await _IMenuTypeRepository.GetAllAsync();
        }

        public LotusRMS_Menu_Type GetByGuid(Guid Id)
        {
            return _IMenuTypeRepository.GetByGuid(Id);
        }

        public async Task<LotusRMS_Menu_Type> GetByGuidAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Guid Update(UpdateTypeDTO dto)
        {
            //using var tx = new TransactionScope();
            var type = _IMenuTypeRepository.GetByGuid(dto.Id) ?? throw new Exception();
            type.Update(dto.Type_Name, dto.Type_Description);

            _IMenuTypeRepository.Update(type);
            _IMenuTypeRepository.Save();
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
            _IMenuTypeRepository.UpdateStatus(Id);

            _IMenuTypeRepository.Save();
            return Id;
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

    }
}
