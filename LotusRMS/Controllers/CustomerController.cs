using DocumentFormat.OpenXml.Office2010.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.CustomerDTO;
using LotusRMS.Models.Dto.DueBookDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Customer;
using LotusRMS.Models.Viewmodels.FiscalYear;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var customer = _customerService.GetAllAvailable();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCustomerVM vm)
        {
            if(!ModelState.IsValid){
                return View(vm);
            }
            var dto = new CreateCustomerDTO()
            {
                Name = vm.Name,
                Address = vm.Address,
                Contact = vm.Contact,
                PanOrVat = vm.PanOrVat
            };
            if (vm.DueAmount > 0)
            {
                var due = new CreateDueBookDTO()
                {
                    DueAmount = vm.DueAmount,

                };
                dto.DueBook = due;
            }
            var id = _customerService.Create(dto);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));

            }
            var customer = _customerService.GetFirstOrDefaultById(id);
            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            float dueBalance = 0;
            if (customer.DueBooks.Count() > 0)
            {
                dueBalance = customer.DueBooks.LastOrDefault().BalanceDue;
            }

            var vm = new UpdateCustomerVM()
            {
                Id=customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Contact = customer.Contact,
                PanOrVat = customer.PanOrVat,
                DueAmount = dueBalance
            };

            return View(vm);

        }

        [HttpPost]
        public IActionResult Update(UpdateCustomerVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dto = new UpdateCustomerDTO()
            {
                Id=vm.Id,
                Name = vm.Name,
                Address = vm.Address,
                Contact = vm.Contact,
                PanOrVat = vm.PanOrVat
            };
            _customerService.Update(dto);
            return RedirectToAction(nameof(Index));
        }
        #region ApiCall
        [HttpGet]
        public IActionResult GetAll()
        {
            var customer = _customerService.GetAllAvailable().Select(cus => new CustomerVm()
            {
                Id=cus.Id,
                Name = cus.Name,
                Address = cus.Address,
                Contact = cus.Contact,
                PanOrVat = cus.PanOrVat,
                DueAmount = GetDue(cus.DueBooks),
                Status=cus.Status
            });
            return Json(new { data = customer });



        }
        public float GetDue(List<LotusRMS_DueBook> dueBook)
        {
            if (dueBook.Count == 0)
            {
                return 0;
            }
            else
            {
                return dueBook.LastOrDefault().BalanceDue;
            }
        }

        [HttpGet]
        public IActionResult StatusChange(Guid id)
        {

            var customer = _customerService.GetByGuid(id);
            if (customer == null)
            {
                return BadRequest();

            }
            else
            {

                _customerService.UpdateStatus(id);

                return Ok(customer.Status);
            }
        }
        #endregion
    }
}
