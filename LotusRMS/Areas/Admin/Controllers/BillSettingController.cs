using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly ICompanyService _iCompanyService;
        private readonly IFiscalYearService _iFiscalYearService;

        private readonly INotyfService _notyf;

        public BillSettingController(IBillSettingService iBillSettingService, ICompanyService iCompanyService,
            IFiscalYearService iFiscalYearService, INotyfService notyf)
        {
            _iBillSettingService = iBillSettingService;
            _iCompanyService = iCompanyService;
            _iFiscalYearService = iFiscalYearService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetBillPrefix(string text)
        {
            string ShortName = "";
            text.Split(' ').ToList().ForEach(i => ShortName += i[0].ToString());
            return ShortName;
        }

        public async Task<IActionResult> Create()
        {
            var company = _iCompanyService.GetCompany();
            if (company.CompanyName == null)
            {
                _notyf.Error("Company not setuped contact developer to register company... ", 5);
                return RedirectToAction(nameof(Index));
            }

            var fiscalyear = await _iFiscalYearService.GetActiveYearAsync();
            if (fiscalyear == null)
            {
                _notyf.Error("Fiscal year not added yet pleas add fiscal year first... ", 5);
                return RedirectToAction(nameof(Index));
            }

            var bill = new CreateBillSettingVM()
            {
                BillTitle = company.CompanyName,
                BillAddress = company.Tole + ", " + company.City,
                BillPrefix = GetBillPrefix(company.CompanyName),
                FiscalYear = fiscalyear.Name,
                PanOrVat = company.PanOrVat,
                Contact = company.Contact,
                IsPhone = true,
                IsPanOrVat = true,
                IsFiscalYear = true,
                BillNote = "Good once sold cannot be returned. this bill is not supported for legal purpose "
            };
            return View(bill);
        }

        [HttpPost]
        public IActionResult Create(CreateBillSettingVM vm)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Some validation error... ", 5);

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
            _notyf.Success("Bill setting added successfully... ", 5);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _notyf.Error("Bill setting id not found ... ", 5);

                return RedirectToAction(nameof(Index));
            }

            var billSetting = _iBillSettingService.GetByGuid(Id);
            if (billSetting == null)
            {
                _notyf.Success("Company not setuped contact developer to register company... ", 5);

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
                _notyf.Error("Some validation error... ", 5);

                return View(vm);
            }

            var billSetting = _iBillSettingService.GetActive();
            if (billSetting.Id == vm.Id && !vm.IsActive)
            {
                _notyf.Error("Active Bill setting cannot be deactive now... ", 5);

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
            _notyf.Success("Bill Setting Updated Successfully... ", 5);

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
                IsFiscalYear = vm.IsFiscalYear,

                IsActive = vm.IsActive
            });

            return Json(new { data = billSettings });
        }

        [HttpGet]
        public IActionResult ActiveChange(Guid id)
        {
            var activeBill = _iBillSettingService.GetActive();
            if (activeBill != null)
            {
                if (activeBill.Id == id)
                {
                    _notyf.Error("Active cannot change... ", 5);

                    return Ok(false);
                }

                var rid = _iBillSettingService.UpdateActive(id);
            }
            _notyf.Success("Active billsetting status changed... ", 5);

            return Ok(true);
        }

        #endregion
    }
}