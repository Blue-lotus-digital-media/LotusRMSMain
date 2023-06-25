using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models.Dto.FiscalYearDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.FiscalYear;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class FiscalYearController : Controller
    {
        private readonly IFiscalYearService _fiscalyearService;
        private readonly INotyfService _notyf;

        public FiscalYearController(IFiscalYearService fiscalyearService, INotyfService notyf)
        {
            _fiscalyearService = fiscalyearService;
            _notyf = notyf;
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
        public async Task<IActionResult> Create(CreateFiscalYearVM vm)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("One or More validation error!!!", 5);
                return View(vm);
            }
            if(await IsDuplicate(vm.Name).ConfigureAwait(false))
            {
                _notyf.Error("Duplicate entry of name", 5);
                return View(vm);
            }


            var dto = new CreateFiscalYearDTO()
            {
                Name = vm.Name,

                StartDateAD = vm.StartDateAD,
                EndDateAD = vm.EndDateAD,
                StartDateBS = vm.StartDateBS,
                EndDateBS = vm.EndDateBS,
                IsActive = vm.IsActive
            };
            

            var id = await _fiscalyearService.CreateAsync(dto).ConfigureAwait(false);
            _notyf.Success("Fiscal year created successfully", 5);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var fiscalyear =await _fiscalyearService.GetByGuidAsync(Id).ConfigureAwait(false);
            if (fiscalyear == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var updateVM = new UpdateFiscalYearVM()
            {
                Id = fiscalyear.Id,
                Name = fiscalyear.Name,
                StartDateAD = fiscalyear.StartDateAD,
                EndDateAD = fiscalyear.EndDateAD,
                StartDateBS = fiscalyear.StartDateBS,
                EndDateBS = fiscalyear.EndDateBS,
                IsActive = fiscalyear.IsActive
            };
            return View(updateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateFiscalYearVM vm)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("One or More validation error", 5);
                return View(vm);
            }
            if (await IsDuplicate(vm.Name, vm.Id).ConfigureAwait(false))
            {
                _notyf.Error("Duplicate Entry for name", 5);
                return View(vm);
            }

            var activeYear = await _fiscalyearService.GetActiveYearAsync();
            if (activeYear.Id == vm.Id && !vm.IsActive)
            {
                _notyf.Error("Must have one activefiscal year !!!", 5);
                return View(vm);
            }

            var dto = new UpdateFiscalYearDTO()
            {
                Id = vm.Id,
                Name = vm.Name,
                StartDateAD = vm.StartDateAD,
                EndDateAD = vm.EndDateAD,
                StartDateBS = vm.StartDateBS,
                EndDateBS = vm.EndDateBS,
                IsActive = vm.IsActive
            };
            var id =await _fiscalyearService.UpdateAsync(dto).ConfigureAwait(false);
            _notyf.Success("Fiscal year updated successfully", 5);
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> IsDuplicate(string Name,Guid Id=new Guid())
        {
            return (await _fiscalyearService.IsDuplicateAsync(Name,Id).ConfigureAwait(false));
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fiscalyears = (await _fiscalyearService.GetAllAvailableAsync().ConfigureAwait(false)).Select(fy => new FiscalYearVM()
            {
                Id = fy.Id,
                Name = fy.Name,
                StartDateAD = fy.StartDateAD,
                EndDateAD = fy.EndDateAD,
                StartDateBS = fy.StartDateBS,
                EndDateBS = fy.EndDateBS,
                Status = fy.Status,
                IsActive = fy.IsActive
            });

            return Json(new { data = fiscalyears });
        }

        [HttpGet]
        public async Task<IActionResult> ActiveChange(Guid id)
        {
            /*var activeYear = await _fiscalyearService.GetActiveYearAsync().ConfigureAwait(true);
            if (activeYear.Id == id)
            {
                _notyf.Error("Active fiscal year cant be changed!!!", 5);
                return Ok(false);
            }*/

            var rid =await _fiscalyearService.UpdateActiveAsync(id).ConfigureAwait(true);
            _notyf.Success("Active fiscal year changed successfully", 5);
            return Ok(true);
        }

        #endregion
    }
}