using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.MenuUnitDTO;
using LotusRMS.Models.Dto.MenuUnitDTO.MenuDivisionDTO;
using LotusRMS.Models.Dto.UnitDto;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.MenuUnit;
using LotusRMS.Models.Viewmodels.Unit;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class MenuUnitController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IMenuUnitService _MenuUnitService;

        public MenuUnitController(INotyfService notyf, IMenuUnitService MenuUnitService)
        {
            _notyf = notyf;
            _MenuUnitService = MenuUnitService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string? returnUrl = null)
        {
            returnUrl ??= nameof(Index);
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUnitVM MenuUnitVM, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                _notyf.Error("Some field cannot be empty!!!", 5);
                return View(MenuUnitVM);
            }
            if (MenuUnitVM.Unit_Division.Count() == 0)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(MenuUnitVM);
            }

            var MenuUnitCreateDto =
                new CreateMenuUnitDTO()
                {
                    UnitName = MenuUnitVM.Unit_Name,
                    UnitSymbol = MenuUnitVM.Unit_Symbol,
                    UnitDescription = MenuUnitVM.Unit_Description,
                    Unit_Division = new List<CreateMenuUnitDivisionDTO>()
                };
            foreach (var item in MenuUnitVM.Unit_Division)
            {
                MenuUnitCreateDto.Unit_Division.Add(new CreateMenuUnitDivisionDTO()
                {
                    Title = item.Title,
                    Value = item.Value
                });
            }
            var id = _MenuUnitService.Create(MenuUnitCreateDto);

            _notyf.Success("Menu Unit created successfully...", 5);
            return Redirect(returnUrl);
        }

        public IActionResult Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                _notyf.Error("Error while loading data! ", 5);
                return RedirectToAction(nameof(Index));
            }

            var unit = _MenuUnitService.GetFirstOrDefaultById((Guid)Id);
            if (unit == null)
            {
                _notyf.Error("No data found !", 5);
                return RedirectToAction(nameof(Index));
            }

            var vm = new UpdateMenuUnitVM()
            {
                Id=unit.Id,
                Unit_Name = unit.Unit_Name,
                Unit_Description = unit.Unit_Description,
                Unit_Symbol = unit.Unit_Symbol
            };
            if (unit.UnitDivision != null | unit.UnitDivision.Count()>0)
            {
                foreach (var item in unit.UnitDivision)
                {
                    vm.Unit_Division.Add(new UpdateMenuUnitDivisionDTO()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Value = item.Value
                    });
                }
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(UpdateMenuUnitVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = new UpdateMenuUnitDTO()
            {
                Id = vm.Id,
                UnitName=vm.Unit_Name,
                UnitDescription=vm.Unit_Description,
                UnitSymbol=vm.Unit_Symbol,

            };
            foreach(var item in vm.Unit_Division)
            {
                dto.Unit_Division.Add(new UpdateMenuUnitDivisionDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Value = item.Value
                });
            }
            _MenuUnitService.Update(dto);

            _notyf.Success("Menu Unit updated successfully...", 5);
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
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "MenuUnit-" + date + ".xlsx");
                }
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var MenuUnits = _MenuUnitService.GetAll().Select(x => new UnitVM()
            {
                Id = x.Id,
                Unit_Name = x.Unit_Name,
                Unit_Symbol = x.Unit_Symbol,
                Unit_Description = x.Unit_Description,
                Status = x.Status
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
                var id = _MenuUnitService.UpdateStatus(Id);

                return Ok(MenuUnit.Status);
            }
        }

        #endregion
    }
}