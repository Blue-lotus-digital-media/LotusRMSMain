using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using LotusRMS.Models.Dto.UnitDto;

using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Unit;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]

    [Area("Admin")]
    public class MenuUnitController : Controller
    {
        private readonly INotyfService _toastNotification;
        private readonly IMenuUnitService _MenuUnitService;

        public MenuUnitController(INotyfService toastNotification, IMenuUnitService MenuUnitService)
        {
            _toastNotification = toastNotification;
            _MenuUnitService = MenuUnitService;
        }

        public IActionResult Index()
        {
           

            
            return View();
        }
        public IActionResult Create(string? returnUrl=null) {
            returnUrl ??= nameof(Index);
            ViewBag.ReturnUrl = returnUrl;

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUnitVM MenuUnitVM,string? returnUrl=null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(MenuUnitVM);
            }

            var MenuUnitCreateDto = new UnitCreateDto(MenuUnitVM.Unit_Name, MenuUnitVM.Unit_Symbol, MenuUnitVM.Unit_Description);
            var id=_MenuUnitService.Create(MenuUnitCreateDto);

            _toastNotification.Success("Menu Unit created successfully...",5);
            return Redirect(returnUrl);
        }

        public IActionResult Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {

                _toastNotification.Error("Error while loading data! ",5);
                return RedirectToAction(nameof(Index));
            }
            var unit = _MenuUnitService.GetByGuid((Guid)Id);
            if (unit == null)
            {
                _toastNotification.Error("No data found !", 5);
                return RedirectToAction(nameof(Index));
            }

            var vm = new UpdateUnitVM()
            {
                Unit_Name = unit.Unit_Name,
                Unit_Description = unit.Unit_Description,
                Unit_Symbol = unit.Unit_Symbol

            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Update(UpdateUnitVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dto = new UnitUpdateDto(
                unitName: vm.Unit_Name,
                unitSymbol: vm.Unit_Symbol,
                unitDescription: vm.Unit_Description
                ) {
                Id = vm.Id
        };
            _MenuUnitService.Update(dto);

            _toastNotification.Success("Menu Unit updated successfully...",5);
            return RedirectToAction(nameof(Index));


        }





        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel()
        {
            var arraylist = _MenuUnitService.GetAll();
        

            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MenuUnit-"+date+".xlsx");
                }
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            var MenuUnits = _MenuUnitService.GetAll().Select(x => new UnitVM()
            {
                Id=x.Id,
                Unit_Name= x.Unit_Name,
                Unit_Symbol=x.Unit_Symbol,
                Unit_Description=x.Unit_Description,
                Status= x.Status
                

            });
            return Json(new { data = MenuUnits });
        }

        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var MenuUnit = _MenuUnitService.GetByGuid(Id);
            if (MenuUnit == null)
            {
                return BadRequest();

            }
            else
            {
              
                var id= _MenuUnitService.UpdateStatus(Id);
                
                return Ok(MenuUnit.Status);
            }
            
        }

        #endregion 

    }
}
