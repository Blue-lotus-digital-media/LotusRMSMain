using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using LotusRMS.DataAccess;
using LotusRMS.Models;
using LotusRMS.Models.Dto.UnitDto;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class ProductUnitController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IUnitService _unitService;

        public ProductUnitController(INotyfService notyf, IUnitService unitService)
        {
            _notyf = notyf;
            _unitService = unitService;
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
        public async Task<IActionResult> Create(UnitVM unitVM, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(unitVM);
            }

            var unitCreateDto = new UnitCreateDto(unitVM.Name, unitVM.Unit_Symbol, unitVM.Unit_Description);
            var id = await _unitService.CreateAsync(unitCreateDto);
            _notyf.Success("Product unit created successfully", 5);
            return Redirect(returnUrl);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ExportToExcel()
        {
            var arraylist  =await _unitService.GetAllAvailableAsync();


            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Unit-" + date + ".xlsx");
                }
            }
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var units =(await _unitService.GetAllAvailableAsync()).Select(x => new UnitVMUpdate()
            {
                Id = x.Id,
                Name = x.Unit_Name,
                Unit_Symbol = x.Unit_Symbol,
                Unit_Description = x.Unit_Description,
                Status = x.Status
            });
            return Json(new { data = units });
        }

        [HttpGet]
        public async Task<IActionResult> StatusChange(Guid Id)
        {
            var unit =await _unitService.GetByGuidAsync(Id);
            if (unit == null)
            {
                return BadRequest();
            }
            else
            {
                var id = await _unitService.UpdateStatusAsync(Id);

                return Ok(unit.Status);
            }
        }

        #endregion
    }
}