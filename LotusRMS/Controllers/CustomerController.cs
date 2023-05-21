using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.Office2010.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.CustomerDTO;
using LotusRMS.Models.Dto.DueBookDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Customer;
using LotusRMS.Models.Viewmodels.DueBook;
using LotusRMS.Models.Viewmodels.FiscalYear;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        private readonly INotyfService _notyf;

        public CustomerController(ICustomerService customerService, INotyfService notyf)
        {
            _customerService = customerService;
            _notyf = notyf;
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
            _notyf.Success("Customer created successfully", 5);

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
            double dueBalance = 0;
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
            _notyf.Success("Product Category updated successfully", 5);
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
                DueAmount = Math.Round(GetDue(cus.DueBooks), 2),
                Status =cus.Status
            });
            return Json(new { data = customer });



        }
        public double GetDue(List<LotusRMS_DueBook> dueBook)
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

                if (customer.Status == true)
                {
                    _notyf.Success("Status Activated successfully..", 2);
                }
                else
                {
                    _notyf.Warning("Status Deactivated...", 2);
                }

                return Ok(customer.Status);
            }
        }


        [HttpGet]
        public IActionResult PayDue(Guid customerId)
        {
            var customer = _customerService.GetFirstOrDefaultById(customerId);
            var vm = new PayDueVM()
            {
                Customer = customer,
                CustomerId = customerId,
                DueAmount = Math.Round(GetDue(customer.DueBooks),2),
                PaidAmount = 0
            };
            return PartialView("_PayDue",model:vm);
        }
        public IActionResult PayDueComplete(PayDueVM vm)
        {
            var customer= _customerService.GetFirstOrDefaultById(vm.CustomerId);
            var dueAmount = GetDue(customer.DueBooks);
            var dueBook = new CreateDueBookDTO()
            {
                DueDate=CurrentTime.DateTimeToday(),
                PaidAmount=vm.PaidAmount,
                DueAmount=dueAmount-vm.PaidAmount,
                BalanceDue=vm.BalanceDue

            };
            var customerDTO = new UpdateCustomerDTO()
            {
                Id = customer.Id,
               DueBook = dueBook

            };
            _customerService.UpdateDue(customerDTO);
            _notyf.Success("Due paid successfully...", 5);
            return Ok();
        }
        #endregion
    }
}
