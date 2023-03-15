
using LotusRMS.Models.Dto.BillSettingDTO;
using LotusRMS.Models.Dto.FiscalYearDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.BillSetting;
using LotusRMS.Models.Viewmodels.FiscalYear;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class BillSettingController : Controller
    {
        private readonly IBillSettingService _iBillSettingService;

        public BillSettingController(IBillSettingService iBillSettingService)
        {
            _iBillSettingService = iBillSettingService;
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
        public IActionResult Create(CreateBillSettingVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dto = new CreateBillSettingDTO()
            {
                BillPrefix = vm.BillPrefix,
                BillAddress = vm.BillAddress,
                BillTitle = vm.BillTitle,
                BillNote = vm.BillNote,
                IsPanOrVat = vm.IsPanOrVat,
                IsPhone = vm.IsPhone,
                IsActive = vm.IsActive

            };
            var id = _iBillSettingService.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var billSetting = _iBillSettingService.GetByGuid(Id);
            if (billSetting == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var updateVM = new UpdateBillSettingVM()
            {
                BillPrefix = billSetting.BillPrefix,
                BillAddress = billSetting.BillAddress,
                BillTitle = billSetting.BillTitle,
                BillNote = billSetting.BillNote,
                IsPanOrVat = billSetting.IsPanOrVat,
                IsPhone = billSetting.IsPhone,
                IsActive = billSetting.IsActive
            };
            return View(updateVM);
        }
        [HttpPost]
        public IActionResult Update(UpdateBillSettingVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var billSetting = _iBillSettingService.GetActive();
            if (billSetting.Id == vm.Id && !vm.IsActive)
            {

                return View(vm);
            }

            var dto = new UpdateBillSettingDTO()
            {
                Id = vm.Id,
                BillPrefix = vm.BillPrefix,
                BillAddress = vm.BillAddress,
                BillTitle = vm.BillTitle,
                BillNote = vm.BillNote,
                IsPanOrVat = vm.IsPanOrVat,
                IsPhone = vm.IsPhone,
                IsActive = vm.IsActive

            };
            var id = _iBillSettingService.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var billSettings = _iBillSettingService.GetAllAvailable().Select(vm => new BillSettingVM()
            {
                Id = vm.Id,
                BillPrefix = vm.BillPrefix,
                BillAddress = vm.BillAddress,
                BillTitle = vm.BillTitle,
                BillNote = vm.BillNote,
                IsPanOrVat = vm.IsPanOrVat,
                IsPhone = vm.IsPhone,
                IsActive = vm.IsActive


            });

            return Json(new { data = billSettings });


        }
        [HttpGet]
        public IActionResult ActiveChange(Guid id)
        {
            var activeBill = _iBillSettingService.GetActive();
            if (activeBill.Id == id)
            {
                return Ok(false);
            }
            var rid = _iBillSettingService.UpdateActive(id);

            return Ok(true);
        }

        #endregion

    }
}
