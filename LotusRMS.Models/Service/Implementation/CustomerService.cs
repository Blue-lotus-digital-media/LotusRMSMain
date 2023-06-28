using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.Dto.CustomerDTO;
using LotusRMS.Models.Helper;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> CreateAsync(CreateCustomerDTO dto)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var customer = new LotusRMS_Customer()
            {
                Name = dto.Name,
                Address = dto.Address,
                Contact = dto.Contact,
                PanOrVat = dto.PanOrVat,
            };
            if (dto.DueBook != null)
            {
                var dueBook = new LotusRMS_DueBook()
                {
                    DueDate = CurrentTime.DateTimeToday(),
                    DueAmount = dto.DueBook.DueAmount,
                    BalanceDue = dto.DueBook.DueAmount
                };
                customer.DueBooks = new List<LotusRMS_DueBook>()
                {
                    dueBook
                };
            }
           await _customerRepository.AddAsync(customer).ConfigureAwait(false);
            scope.Complete();
            return customer.Id;
        }

        public async Task<IEnumerable<LotusRMS_Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LotusRMS_Customer>> GetAllAvailableAsync()
        {
            return await _customerRepository.GetAllAsync(x => !x.IsDelete && x.Status,includeProperties: "DueBooks,DueBooks.Invoice").ConfigureAwait(false);
        }

        public async Task<LotusRMS_Customer?> GetByGuidAsync(Guid id)
        {
            return await _customerRepository.GetByGuidAsync(id).ConfigureAwait(false);
        }

        public async Task<LotusRMS_Customer?> GetFirstOrDefaultByIdAsync(Guid id)
        {
            return await _customerRepository.GetFirstOrDefaultAsync(x => x.Id==id, includeProperties: "DueBooks").ConfigureAwait(false);
        }

        public async Task UpdateAsync(UpdateCustomerDTO dto)
        {
            var customer = new LotusRMS_Customer()
            {
                Id=dto.Id,
                PanOrVat = dto.PanOrVat
        };
            customer.Update(name: dto.Name, address: dto.Address, contact: dto.Contact);
           await _customerRepository.UpdateAsync(customer).ConfigureAwait(false);
        }

        public async Task UpdateDueAsync(UpdateCustomerDTO dto)
        {
            var customer = new LotusRMS_Customer()
            {
                Id = dto.Id,
                DueBooks=new List<LotusRMS_DueBook>()
                {
                    new LotusRMS_DueBook()
                    {
                        DueDate=CurrentTime.DateTimeToday(),
                        PaidAmount=dto.DueBook.PaidAmount,
                        Invoice_Amount=0,
                        DueAmount=0,
                        BalanceDue=dto.DueBook.BalanceDue
                    }
                }
                
            };
            await _customerRepository.UpdateDueAsync(customer).ConfigureAwait(false);
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
           await _customerRepository.UpdateStatusAsync(Id).ConfigureAwait(false);
        }
    }
}
