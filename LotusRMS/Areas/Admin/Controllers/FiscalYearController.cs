
using LotusRMS.Models.Dto.FiscalYearDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.FiscalYear;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class FiscalYearController : Controller
    {
        private readonly IFiscalYearService _fiscalyearService;

        public FiscalYearController(IFiscalYearService fiscalyearService)
        {
            _fiscalyearService = fiscalyearService;
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
        public IActionResult Create(CreateFiscalYearVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dto = new CreateFiscalYearDTO()
            {
                Name = vm.Name,

                StartDateAD = vm.StartDateAD,
                EndDateAD=vm.EndDateAD,
                StartDateBS=vm.StartDateBS,
                EndDateBS=vm.EndDateBS,
                IsActive=vm.IsActive

            };
            var id=_fiscalyearService.Create(dto);

            return RedirectToAction(nameof(Index));
        } 
             
        public IActionResult Update(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var fiscalyear = _fiscalyearService.GetByGuid(Id);
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
        public IActionResult Update(UpdateFiscalYearVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var activeYear = _fiscalyearService.GetActiveYear();
            if (activeYear.Id == vm.Id && !vm.IsActive)
            {

                return View(vm);
            }

            var dto = new UpdateFiscalYearDTO()
            {
                Id=vm.Id,
                Name = vm.Name,
                StartDateAD = vm.StartDateAD,
                EndDateAD = vm.EndDateAD,
                StartDateBS = vm.StartDateBS,
                EndDateBS = vm.EndDateBS,
                IsActive = vm.IsActive

            };
            var id = _fiscalyearService.Update(dto);

            return RedirectToAction(nameof(Index));
        }


        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var fiscalyears = _fiscalyearService.GetAllAvailable().Select(fy => new FiscalYearVM()
            {
                Id=fy.Id,
                Name=fy.Name,
                StartDateAD=fy.StartDateAD,
                EndDateAD=fy.EndDateAD,
                StartDateBS=fy.StartDateBS,
               EndDateBS=fy.EndDateBS,
               Status=fy.Status,
               IsActive=fy.IsActive


            }); 
                
                return Json(new { data = fiscalyears }); 


        }
        [HttpGet]
        public IActionResult ActiveChange(Guid id)
        {
            var activeYear = _fiscalyearService.GetActiveYear();
            if (activeYear.Id == id)
            {
                return Ok(false);
            }
            var rid=_fiscalyearService.UpdateActive(id);
            
            return Ok(true);
        }

#endregion

    }
}
