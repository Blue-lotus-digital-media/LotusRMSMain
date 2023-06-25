using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.Viewmodels.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ICompanyService
    {
       Task<UpdateCompanyVM?> GetCompanyAsync();
        Task<Guid> CreateAsync(CreateCompanyDTO dto);
        Task<Guid> UpdateAsync(UpdateCompanyDTO dto);
        Task<string> GetCompanyNameAsync();
        Task UpdateIpAsync(string Ip);
        Task<string> GetIpAsync();

        


    }
}
