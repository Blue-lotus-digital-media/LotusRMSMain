using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Menu;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Models.Viewmodels.QrTable;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LotusRMSweb.Areas.Menu.Controllers
{
    [Area("Menu")]
    public class HomeController : Controller
    {
        private readonly ITableService _iTableService;
        private readonly ICompanyService _iCompanyService;

        private readonly IMenuService _iMenuService;
        public HomeController(ITableService iTableService,
            IMenuService iMenuService,
            ICompanyService iCompanyService)
        {
            _iTableService = iTableService;
            _iMenuService = iMenuService;
            _iCompanyService = iCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid TableNo)
        {
            var connecton = HttpContext.Connection;
            var remoteIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var systemIp = await _iCompanyService.GetIp();
           if (systemIp != remoteIp)
            {
                return BadRequest();
            }
            ViewBag.IpSet = "remote Ip =" + remoteIp + " , system Ip= " + systemIp;

            var customer = new List<string>();
            if (HttpContext.Session.GetString("customerInfo") != null)
            {
              customer= JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("customerInfo"));
            }

                var table = await _iTableService.GetFirstOrDefaultByIdAsync(TableNo);
            if (table != null)
            {
                var menus = _iMenuService.GetAllAvailable().ToList().Select(menu => new MenuVM()
                {
                    Id = menu.Id,
                    Item_name = menu.Item_Name,
                    OrderTo = menu.OrderTo,
                    Menu_Unit_Name = menu.Menu_Unit.Unit_Symbol,
                    Menu_Category_Name = menu.Menu_Category.Category_Name,
                    Menu_Type_Name = menu.Menu_Type.Type_Name,
                    Menu_Image = ImageUpload.GetStrigFromByteArray(menu.Image),
                    Status = menu.Status,
                    MenuDetail = menu.Menu_Details.Select(md => new MenuDetailVM()
                    {
                        Id = md.Id,
                        Quantity = md.Divison.Title + "(" + md.Divison.Value + " " + menu.Menu_Unit.Unit_Symbol + ")",
                        Rate = md.Rate,
                        IsDefault = md.Default
                    }).ToList()
                }).ToList();

                var qrTableVM = new QrTableMenuVM()
                {
                    Table_Name=table.Table_Name,
                    Table_No=table.Table_No,
                    Table_Id=table.Id,
                    Menus=menus
                    
                };
                return View(qrTableVM);




            }
            return NotFound();
        }
    }
}
