using LotusRMS.Models.Dto.FiscalYearDTO;
using LotusRMS.Models.IRepositorys;

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusRMS.Models.Helper;

namespace LotusRMS.Models.Service.Implementation
{
    public class FiscalYearService : IFiscalYearService
    {
        private readonly IFiscalYearRepository _fiscalYearRepository;

        public FiscalYearService(IFiscalYearRepository fiscalYearRepository)
        {
            _fiscalYearRepository = fiscalYearRepository;
        }

        public async Task<bool> CheckActiveAsync(Guid Id)
        {
            var year = await _fiscalYearRepository.GetByGuidAsync(Id);
            if (year.IsActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Guid> CreateAsync(CreateFiscalYearDTO dto)
        {
           
                var FY = new LotusRMS_FiscalYear()
                    {
                        Name = dto.Name,
                        StartDateAD = dto.StartDateAD.ToString(),
                        EndDateAD = dto.EndDateAD.ToString(),
                        StartDateBS = dto.StartDateBS,
                        EndDateBS = dto.EndDateBS,
                        IsActive = dto.IsActive
                    };
                    await _fiscalYearRepository.AddAsync(FY);

                    if (dto.IsActive)
                    {
                        var years = await _fiscalYearRepository.GetFirstOrDefaultAsync(x => x.IsActive && x.Id != FY.Id);
                        if (years != null)
                        {
                            years.IsActive = false;
                        }

                    }
                   await _fiscalYearRepository.SaveAsync();
                    

                    return FY.Id;
                

            

        }

        public async Task<IEnumerable<LotusRMS_FiscalYear>> GetAllAsync()
        {
            return await _fiscalYearRepository.GetAllAsync();
        }

        public async Task<IEnumerable<LotusRMS_FiscalYear>> GetAllAvailableAsync()
        {
            return await _fiscalYearRepository.GetAllAsync(x => !x.IsDelete);
        }

        public async Task<LotusRMS_FiscalYear> GetByGuidAsync(Guid Id)
        {
            return await _fiscalYearRepository.GetFirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<LotusRMS_FiscalYear> GetActiveYearAsync()
        {
            return await _fiscalYearRepository.GetFirstOrDefaultAsync(x => x.IsActive);
        }

        public async Task<Guid> UpdateAsync(UpdateFiscalYearDTO dto)
        {
            
            var fiscalYear = new LotusRMS_FiscalYear();
            fiscalYear.Id = dto.Id;
            fiscalYear.StartDateAD = dto.StartDateAD;
            fiscalYear.StartDateBS = dto.StartDateBS;
            fiscalYear.EndDateAD = dto.EndDateAD;
            fiscalYear.EndDateBS = dto.EndDateBS;
            fiscalYear.IsActive = dto.IsActive;
           await _fiscalYearRepository.UpdateAsync(fiscalYear);
                   
            return dto.Id;
             

        }

        public async Task<Guid> UpdateActiveAsync(Guid Id)
        {
           
                await _fiscalYearRepository.UpdateActiveAsync(Id).ConfigureAwait(false);
                    return Id;
              
          

           
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsDuplicateAsync(string FiscalYear, Guid? Id)
        {
            var fiscalyear = await _fiscalYearRepository.GetFirstOrDefaultAsync(x => x.Name == FiscalYear && !x.IsDelete).ConfigureAwait(false);
            if(fiscalyear == null)
            {
                return false;
            }
            else
            {
                if (Id != Guid.Empty && fiscalyear.Id==Id)
                {
                    return false;
                }
                return true;
            }
        }

       /* Task<bool> IFiscalYearService.IsDuplicate(string FiscalYear)
        {
            throw new NotImplementedException();
        }*/
    }
}
