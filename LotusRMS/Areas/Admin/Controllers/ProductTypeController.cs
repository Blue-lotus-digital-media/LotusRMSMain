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
        public async Task<IActionResult> Create(CreateTypeVM type, string? returnUrl = null)
        {
            returnUrl ??= nameof(Index);
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(type);
            }
            if (await IsDuplicateName(type.Type_Name))
            {
                _notyf.Error("Duplicate Entry for type name " + type.Type_Name);
                return View(type);
            }
            var dto = new CreateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description);
            await _ITypeService.CreateAsync(dto);
            _notyf.Success("Product Type Created Successfully!",5);
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Update(Guid Id=new Guid())

        {
            if (Id == Guid.Empty)
            {
                return BadRequest("No data Found");
            }
            var type =await _ITypeService.GetByGuidAsync((Guid)Id);
            var updateTypeVM = new UpdateTypeVM()
            {
                Id = type.Id,
                Type_Description = type.Type_Description,
                Type_Name = type.Type_Name
            };

            return View(updateTypeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTypeVM type)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }
            if(await IsDuplicateName(type.Type_Name, type.Id))
            {
                _notyf.Error("Duplicate Entry for type name " + type.Type_Name);
                return View(type);
            }

            var dto = new UpdateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description)
            {
                Id = type.Id
            };


            await _ITypeService.UpdateAsync(dto);
            _notyf.Success("Product Type Updated Successfully!",5);


            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsDuplicateName(string name,Guid Id=new Guid())
        {
            return await _ITypeService.IsDuplicateName(name, Id);

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ExportToExcel()
        {
            var arraylist = await _ITypeService.GetAllAvailableAsync();


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
        public async Task<IActionResult> GetAll()
        {
            var types = (await _ITypeService.GetAllAvailableAsync()).Select(x => new TypeVM()
            {
                Id = x.Id,
                Type_Name = x.Type_Name,
                Type_Description = x.Type_Description,
                Status = x.Status
            });
            return Json(new { data = types });
        }

        [HttpGet]
        public async Task<IActionResult> StatusChange(Guid Id)
        {
            var unit = await _ITypeService.GetByGuidAsync(Id);
            if (unit == null)
            {
                return BadRequest();
            }
            else
            {
                var id =await _ITypeService.UpdateStatusAsync(Id);

                return Ok(unit.Status);
            }
        }

        #endregion
    }
}