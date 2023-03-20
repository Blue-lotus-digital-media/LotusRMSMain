using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.Dto.CustomerDTO;
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

        public Guid Create(CreateCustomerDTO dto)
        {
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
            _customerRepository.Add(customer);
            _customerRepository.Save();
            return customer.Id;
        }

        public IEnumerable<LotusRMS_Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LotusRMS_Customer> GetAllAvailable()
        {
            return _customerRepository.GetAll(x => !x.IsDelete && x.Status,includeProperties: "DueBooks,DueBooks.Invoice");
        }

        public LotusRMS_Customer GetByGuid(Guid id)
        {
            return _customerRepository.GetByGuid(id);
        }

        public LotusRMS_Customer GetFirstOrDefaultById(Guid id)
        {
            return _customerRepository.GetFirstOrDefault(x => x.Id==id, includeProperties: "DueBooks");
        }

        public void Update(UpdateCustomerDTO dto)
        {
            var customer = new LotusRMS_Customer()
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Contact = dto.Contact,
                PanOrVat = dto.PanOrVat

            };
            _customerRepository.Update(customer);
        }

        public void UpdateStatus(Guid Id)
        {
            _customerRepository.UpdateStatus(Id);
        }
    }
}
