using LotusRMS.Models.Dto.SupplierDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
    public class SupplierController : Controller
    {
        public readonly ISupplierService _iSupplierService;

        public SupplierController(ISupplierService iSupplierService)
        {
            _iSupplierService = iSupplierService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSupplierVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dto = new CreateSupplierDTO()
            {
                FullName = vm.FullName,
                Address = vm.Address,
                Contact = vm.Contact,
                Contact1 = vm.Contact1,
                PanOrVat = vm.PanOrVat
            };
            var id = _iSupplierService.Create(dto);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var supplier = _iSupplierService.GetByGuid(Id);
            if (supplier == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var vm = new UpdateSupplierVM()
            {
                Id= supplier.Id,
                FullName = supplier.FullName,
                Address = supplier.Address,
                Contact = supplier.Contact,
                Contact1 = supplier.Contact1,
                PanOrVat = supplier.PanOrVat
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Update(UpdateSupplierVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dto = new UpdateSupplierDTO()
            {
                Id=vm.Id,
                FullName = vm.FullName,
                Address = vm.Address,
                Contact = vm.Contact,
                Contact1 = vm.Contact1,
                PanOrVat = vm.PanOrVat
            };
            _iSupplierService.Update(dto);

            return RedirectToAction(nameof(Index));
        }
        #region APi Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var suppliers = _iSupplierService.GetAll().Select(sup => new SupplierVM()
            {
                Id = sup.Id,
                FullName = sup.FullName,
                Address = sup.Address,
                Contact = sup.Contact,
                Contact1 = sup.Contact1,
                PanOrVat = sup.PanOrVat,
                Status = sup.Status

            });
            return Json(new { data = suppliers });
        }
        [HttpGet]
        public IActionResult StatusChange(Guid id)
        {

            var supplier = _iSupplierService.GetByGuid(id);
            if (supplier == null)
            {
                return BadRequest();
            }
            else
            {

                _iSupplierService.UpdateStatus(id);

                return Ok(supplier.Status);
            }
        }

        #endregion
    }
}
