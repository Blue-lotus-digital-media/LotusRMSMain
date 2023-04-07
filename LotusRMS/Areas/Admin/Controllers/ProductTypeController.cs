using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using LotusRMS.Models.Dto.TypeDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Type;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class ProductTypeController : Controller
    {
        public readonly ITypeService _ITypeService;
        private readonly INotyfService _notyf;

        public ProductTypeController(ITypeService iTypeService,INotyfService notyf)
        {
            _ITypeService = iTypeService;
            _notyf = notyf;
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
        public IActionResult Create(CreateTypeVM type, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }

            var dto = new CreateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description);


            _ITypeService.Create(dto);
            _notyf.Success("Product Type Created Successfully!",5);


            returnUrl ??= nameof(Index);

            return Redirect(returnUrl);
        }

        public IActionResult Update(Guid? Id)

        {
            if (Id == Guid.Empty)
            {
                return BadRequest("No data Found");
            }

            var type = _ITypeService.GetByGuid((Guid)Id);

            var updateTypeVM = new UpdateTypeVM()
            {
                Id = type.Id,
                Type_Description = type.Type_Description,
                Type_Name = type.Type_Name
            };

            return View(updateTypeVM);
        }

        [HttpPost]
        public IActionResult Update(UpdateTypeVM type)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }

            var dto = new UpdateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description)
            {
                Id = type.Id
            };


            _ITypeService.Update(dto);
            _notyf.Success("Product Type Updated Successfully!",5);


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel()
        {
            var arraylist = _ITypeService.GetAll();


            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ProductType-" + date + ".xlsx");
                }
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var types = _ITypeService.GetAll().Select(x => new TypeVM()
            {
                Id = x.Id,
                Type_Name = x.Type_Name,
                Type_Description = x.Type_Description,
                Status = x.Status
            });
            return Json(new { data = types });
        }

        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var unit = _ITypeService.GetByGuid(Id);
            if (unit == null)
            {
                return BadRequest();
            }
            else
            {
                var id = _ITypeService.UpdateStatus(Id);

                return Ok(unit.Status);
            }
        }

        #endregion
    }
}