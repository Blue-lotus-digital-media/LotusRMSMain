using ClosedXML.Excel;
using LotusRMS.Models.Dto.TableDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Table;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class TableController : Controller
    {

        public readonly ITableService _ITableService;

        public readonly ITableTypeService _ITableTypeService;

        public TableController(ITableService iTableService, ITableTypeService iTableTypeService)
        {
            _ITableService = iTableService;
            _ITableTypeService = iTableTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var tableVM = new CreateTableVM();
            tableVM.Table_Type_List = _ITableTypeService.GetAll().Select(type => new SelectListItem()
            {
                Text = type.Type_Name,
                Value = type.Id.ToString()

            }).ToList();

            return View(tableVM);
        }
        [HttpPost]
        public IActionResult Create(CreateTableVM tableVM)
        {
            tableVM.Table_Type_List = _ITableTypeService.GetAll().Select(type => new SelectListItem()
            {
                Text = type.Type_Name,
                Value = type.Id.ToString()

            }).ToList();
            if (!ModelState.IsValid)
            {
                return View(tableVM);
            }
            var dto = new CreateTableDTO(
                table_Name: tableVM.Table_Name,
                table_No: tableVM.Table_No,
                no_Of_Chair: tableVM.No_Of_Chair,
                table_Type_Id: tableVM.Table_Type_Id);
            _ITableService.Create(dto);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var table = _ITableService.GetByGuid((Guid)Id);
            if (table == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var tableVM = new UpdateTableVM() { 

                Table_Name= table.Table_Name,
                Table_No= table.Table_No,
                No_Of_Chair= table.No_Of_Chair,
                Table_Type_Id= table.Table_Type_Id,
            

                Id = table.Id

            };
            tableVM.Table_Type_List = _ITableTypeService.GetAll().Select(type => new SelectListItem()
            {
                Text = type.Type_Name,
                Value = type.Id.ToString()

            }).ToList();
            return View(tableVM);
        }
        [HttpPost]
        public IActionResult Update(UpdateTableVM tableVM)
        {
            if (tableVM.Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                tableVM.Table_Type_List = _ITableTypeService.GetAll().Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()

                }).ToList();
                return View(tableVM);
            }
            var dto = new UpdateTableDTO(
                table_Name: tableVM.Table_Name,
                table_No: tableVM.Table_No,

                no_Of_Chair: tableVM.No_Of_Chair,
                table_Type_Id: tableVM.Table_Type_Id)
            {
                Id=tableVM.Id
            };
            _ITableService.Update(dto);
            return RedirectToAction(nameof(Index));

        }
        #region API CALLS
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel()
        {
            var arraylist = _ITableService.GetAll();


            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Table-" + date + ".xlsx");
                }
            }
        }


        [HttpGet]
        public IActionResult GetAll()
        {

            var categorys = _ITableService.GetAll().Select(x => new TableVM()
            {
                Id = x.Id,
                Table_Name = x.Table_Name,
                Table_No = x.Table_No,
                No_Of_Chair = x.No_Of_Chair,
                Table_Type_Name = x.Table_Type.Type_Name,
                Status = x.Status,
                IsReserved = x.IsReserved,
                IsDelete = x.IsDelete



            });
            return Json(new { data = categorys });
        }

        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var category = _ITableService.GetByGuid(Id);
            if (category == null)
            {
                return BadRequest();

            }
            else
            {

                _ITableService.UpdateStatus(Id);

                return Ok(category.Status);
            }

        }

        #endregion

    }
}
