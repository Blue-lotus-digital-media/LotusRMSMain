using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace LotusRMS.DataAccess.Repository
{
    public class CompanyRepository : BaseRepository<LotusRMS_Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _dal;
        public CompanyRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task UpdateAsync(LotusRMS_Company obj)
        {
            var company =await GetFirstOrDefaultAsync().ConfigureAwait(false);
            company.Update(
            companyName: obj.CompanyName,
            country: obj.Country,
            province: obj.Province,
            city: obj.City,
            tole: obj.Tole,
            email: obj.Email,   
            contact:obj.Contact,
            panOrVat: obj.PanOrVat,
            contactPersons: obj.ContactPersons,
            registrationDate: obj.RegistrationDate,
            validTill: obj.ValidTill,
             companyRegistrationNumber : obj.CompanyRegistrationNumber,
            contractDate: obj.ContractDate,
            serviceStartDate: obj.ServiceStartDate,
            registrationNo:obj.RegistrationNo
            

            );
            company.WebSite = obj.WebSite;
            await SaveAsync();
        }

      
    }
}
