﻿using AspNetCoreHero.ToastNotification.Abstractions;
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
    public class TableTypeController : Controller
    {
        public readonly ITableTypeService _ITableTypeService;

        private readonly INotyfService _notyf;

        public TableTypeController(ITableTypeService iTableTypeService, INotyfService notyf)
        {
            _ITableTypeService = iTableTypeService;
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
            if (!ModelState.IsValid)
            {
                return View(type);
            }

            if (!(await  _ITableTypeService.IsExist(type.Type_Name)))
            {

                var dto = new CreateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description);


                _ITableTypeService.Create(dto);

                _notyf.Success("Table Type created successfully !", 5);


                returnUrl ??= nameof(Index);

                return Redirect(returnUrl);
            }
            else
            {
                _notyf.Warning("Table Type with name "+ type.Type_Name + " already exist. Please try new Name!", 5);
                return View(type);
            }
        }

        public IActionResult Update(Guid? Id)

        {
            if (Id == Guid.Empty)
            {
                return BadRequest("No data Found");
            }

            var type = _ITableTypeService.GetByGuid((Guid)Id);

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
            if (!(await _ITableTypeService.IsExist(type.Type_Name)))
            {
                var dto = new UpdateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description)
                {
                    Id = type.Id
                };
                _ITableTypeService.Update(dto);
                _notyf.Success("Table Type updated successfully !", 5);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _notyf.Warning("Table Type with name " + type.Type_Name + " already exist. Please try new Name!", 5);
                return View(type);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel()
        {
            var arraylist = _ITableTypeService.GetAll();


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
            var types = _ITableTypeService.GetAll().Select(x => new TypeVM()
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
            var unit = _ITableTypeService.GetByGuid(Id);
            if (unit == null)
            {
                return BadRequest();
            }
            else
            {
                var id = _ITableTypeService.UpdateStatus(Id);

                return Ok(unit.Status);
            }
        }

        #endregion
    }
}