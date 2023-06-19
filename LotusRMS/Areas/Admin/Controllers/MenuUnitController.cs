using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.MenuUnitDTO;
using LotusRMS.Models.Dto.MenuUnitDTO.MenuDivisionDTO;
using LotusRMS.Models.Dto.UnitDto;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
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
        public async Task<IActionResult> Create(CreateUnitVM MenuUnitVM, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                _notyf.Error("Some field cannot be empty!!!", 5);
                return View(MenuUnitVM);
            }
            if (await IsDuplicate(MenuUnitVM.Unit_Name).ConfigureAwait(false))
            {
                ViewBag.ReturnUrl = returnUrl;
                _notyf.Error("Duplicate entry for name "+MenuUnitVM.Unit_Name+" !!!", 5);
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
            var id =await _MenuUnitService.CreateAsync(MenuUnitCreateDto).ConfigureAwait(false);

            _notyf.Success("Menu Unit created successfully...", 5);
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                _notyf.Error("Error while loading data! ", 5);
                return RedirectToAction(nameof(Index));
            }

            var unit = await _MenuUnitService.GetFirstOrDefaultByIdAsync((Guid)Id).ConfigureAwait(false);
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
        public async Task<IActionResult> Update(UpdateMenuUnitVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (await IsDuplicate(vm.Unit_Name,vm.Id).ConfigureAwait(false))
            {
             
                _notyf.Error("Duplicate entry for name " + vm.Unit_Name + " !!!", 5);
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
            await _MenuUnitService.UpdateAsync(dto).ConfigureAwait(false);

            _notyf.Success("Menu Unit updated successfully...", 5);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ExportToExcel()
        {
            var arraylist = await _MenuUnitService.GetAllAsync().ConfigureAwait(false);


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

        private async Task<bool> IsDuplicate(string Name, Guid Id = new Guid())
        {
            return (await _MenuUnitService.IsDuplicateAsync(Name, Id).ConfigureAwait(false));
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var MenuUnits = (await _MenuUnitService.GetAllAsync().ConfigureAwait(false)).Select(x => new UnitVM()
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
        public async Task<IActionResult> StatusChange(Guid Id)
        {
            var MenuUnit = await _MenuUnitService.GetByGuidAsync(Id).ConfigureAwait(false);
            if (MenuUnit == null)
            {
                return BadRequest();
            }
            else
            {
                var id = await _MenuUnitService.UpdateStatusAsync(Id).ConfigureAwait(false);
                if (MenuUnit.Status == true)
                {
                    _notyf.Success("Status Activated successfully..", 2);
                }
                else
                {
                    _notyf.Warning("Status Deactivated...", 2);
                }

                return Ok(MenuUnit.Status);
            }
        }

        #endregion
    }
}