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
using NToastNotify;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    /*[Authorize(Roles ="Admin")]*/

    [Area("Admin")]
    public class UnitController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IUnitService _unitService;

        public UnitController(IToastNotification toastNotification, IUnitService unitService)
        {
            _toastNotification = toastNotification;
            _unitService = unitService;
        }

        public IActionResult Index()
        {
            //var units = _IUnitOfWork.Unit.GetAll();
           

            _toastNotification.AddInfoToastMessage("Added for test");
            return View(/*units*/);
        }
        public IActionResult Create() {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UnitVM unitVM)
        {
            if (!ModelState.IsValid)
            {
                return View(unitVM);
            }

            var unitCreateDto = new UnitCreateDto(unitVM.Name, unitVM.Unit_Symbol, unitVM.Unit_Description);
            var id=_unitService.Create(unitCreateDto);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel()
        {
            var arraylist = _unitService.GetAll();
        

            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Unit-"+date+".xlsx");
                }
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            var units = _unitService.GetAll().Select(x => new UnitVMUpdate()
            {
                Id=x.Id,
                Name= x.Unit_Name,
                Unit_Symbol=x.Unit_Symbol,
                Unit_Description=x.Unit_Description,
                Status= x.Status
                

            });
            return Json(new { data = units });
        }

        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var unit = _unitService.GetByGuid(Id);
            if (unit == null)
            {
                return BadRequest();

            }
            else
            {
              
                var id= _unitService.UpdateStatus(Id);
                
                return Ok(unit.Status);
            }
            
        }

        #endregion 

    }
}
