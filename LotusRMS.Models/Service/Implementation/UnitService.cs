using LotusRMS.Models.Dto.UnitDto;
using LotusRMS.Models.Helper;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LotusRMS.Models.Service.Implementation
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<Guid> CreateAsync(UnitCreateDto dto)
        {
            using var tx = TransactionScopeHelper.GetInstance;
            var unit = new LotusRMS_Unit(dto.UnitName, dto.UnitSymbol, dto.UnitDescription);
            await _unitRepository.AddAsync(unit);
            await _unitRepository.SaveAsync();
            tx.Complete();
            return unit.Id;


        }

        public async Task<Guid> UpdateAsync(UnitUpdateDto dto)
        {
            using var tx = TransactionScopeHelper.GetInstance; ;
            var unit = await _unitRepository.GetByGuidAsync(dto.Id) ?? throw new Exception();

            unit.Update(unit_Name: dto.UnitName, unit_Symbol: dto.UnitSymbol, unit_Description: dto.UnitDescription);
           
            await _unitRepository.UpdateAsync(unit).ConfigureAwait(false);
            
            //todo logic

            tx.Complete();
            return unit.Id;
        }

        public async Task<IEnumerable<LotusRMS_Unit>> GetAllAsync()
        {
            return await _unitRepository.GetAllAsync().ConfigureAwait(false);
        }  
        public async Task<IEnumerable<LotusRMS_Unit>> GetAllAvailableAsync()
        {
            return await _unitRepository.GetAllAsync(x=>!x.IsDelete && x.Status).ConfigureAwait(false);
        } 
        public async Task<LotusRMS_Unit?> GetByGuidAsync(Guid Id)
        {
            return await _unitRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            using var tx = TransactionScopeHelper.GetInstance;
            await _unitRepository.UpdateStatusAsync(Id).ConfigureAwait(false);
            tx.Complete();
            return Id;

        }
    }
}
