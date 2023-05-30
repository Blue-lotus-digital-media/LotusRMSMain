using ClosedXML.Excel;
using DinkToPdf;
using DinkToPdf.Contracts;
using LotusRMS.Models.Dto.TableDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Table;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.EMMA;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class TableController : Controller
    {
        private readonly IConverter _converter;

        public readonly ITableService _ITableService;

        public readonly ITableTypeService _ITableTypeService;

        private readonly INotyfService _notyf;
        private readonly ICompanyService _ICompanyService;

        public TableController(ITableService iTableService, 
            ITableTypeService iTableTypeService, 
            IConverter converter,
            INotyfService notyf, 
            ICompanyService iCompanyService)
        {
            _ITableService = iTableService;
            _ITableTypeService = iTableTypeService;
            _converter = converter;
            _notyf = notyf;
            _ICompanyService = iCompanyService;
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

            _notyf.Success("Table created successfully !", 5);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(Guid? Id)
        {
            if (Id == Guid.Empty || Id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var table = _ITableService.GetByGuid((Guid)Id);
            if (table == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var tableVM = new UpdateTableVM()
            {
                Table_Name = table.Table_Name,
                Table_No = table.Table_No,
                No_Of_Chair = table.No_Of_Chair,
                Table_Type_Id = table.Table_Type_Id,


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
                Id = tableVM.Id
            };
            _ITableService.Update(dto);

            _notyf.Success("Table updated successfully...", 5);

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult DownloadQr(Guid Id)
        {
            String strUrl = HttpContext.Request.Path;
            /*  String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            */
            var table = _ITableService.GetByGuid(Id);
            if (table == null)
            {
                return BadRequest("Table not found");
            }

            var stringImages = GetQR(Id);
            var companyName = _ICompanyService.GetCompany().CompanyName;


            return Ok(new { hotelName = companyName, tableName = table.Table_Name, stringImage = stringImages });
        }

        public string GetQR(Guid Id)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var callbackUrl = Url.Action("Index", "Home", new { area = "menu", TableNo = Id }, Request.Scheme);


            QRCodeData qrCodeData = qrGenerator.CreateQrCode(callbackUrl, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20); //, "#000ff0", "#0ff000"); for color
            var stringImage = ImageUpload.GetStrigFromByteArray(qrCodeAsBitmapByteArr);
            return stringImage;
        }


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
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Table-" + date + ".xlsx");
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
                if (category.Status == true)
                {
                    _notyf.Success("Status Activated successfully..", 2);  
                }
                else
                {
                    _notyf.Warning("Status Deactivated...",2);
                }
                return Ok(category.Status);
               
            }
        }

        public IActionResult DownloadAllQr()
        {
            var companyName = _ICompanyService.GetCompany().CompanyName;

            var tables = _ITableService.GetAll().Where(x => x.Status && !x.IsDelete).Select(tbl => new QrTableVM()
            {
                Table_Name = tbl.Table_Name,
                imageString = GetQR(tbl.Id)
            }).ToList();
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "QRCodeTable",
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(tables),
                WebSettings =
                {
                    DefaultEncoding = "utf-8",
                    UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css")
                },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = companyName +": QR Code "}
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "QrTables.pdf");
        }

        #endregion
    }
}