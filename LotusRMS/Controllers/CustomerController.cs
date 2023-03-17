using LotusRMS.Models.Dto.CustomerDTO;
using LotusRMS.Models.Dto.DueBookDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Customer;
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

    }
}
