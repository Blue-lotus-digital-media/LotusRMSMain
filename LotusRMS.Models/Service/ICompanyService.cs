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
        UpdateCompanyVM GetCompany();
        Guid Create(CreateCompanyDTO dto);
        Task<Guid> Update(UpdateCompanyDTO dto);

        Task UpdateIp(string Ip);
        Task<string> GetIp();

        


    }
}
