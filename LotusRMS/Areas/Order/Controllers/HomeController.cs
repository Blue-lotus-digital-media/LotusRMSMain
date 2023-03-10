using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LotusRMSweb.Areas.Order.Controllers
{
    [Area("Order")]
    [Authorize(Roles ="Admin,SuperAdmin,Waiter,Cashier,Bar")]
    public class HomeController : Controller
    {

        public readonly ITableTypeService _ITableTypeService;
        public readonly ITableService _ITableService;
        public readonly IMenuService _IMenuService;
        public HomeController(ITableTypeService TableTypeService, ITableService iTableService, IMenuService iMenuService)
        {
            _ITableTypeService = TableTypeService;
            _ITableService = iTableService;
            _IMenuService = iMenuService;
        }


        public IActionResult Index()
        {
            var type = _ITableTypeService.GetAll().Where(x => !x.IsDelete && x.Status);
            var menu = _IMenuService.GetAll().Where(x => !x.IsDelete && x.Status).Select(menu => new SelectListItem()
            {
                Text = menu.Item_Name,
                Value = menu.Id.ToString()
            }).ToList();
            ViewBag.Menu = menu;

            return View(type);
        }
        public IActionResult GetTable(Guid Id)
        {
            var table= _ITableService.GetAll().Where(x => !x.IsDelete && x.Status && x.Table_Type_Id==Id);
            return PartialView("_TableList", model: table);
        }

        public IActionResult GetOrder(Guid Id)
        {

            return PartialView("_Order",model:Id
                );
        }
        public IActionResult Selectmenu(Guid MenuId)
        {
            var menu=_IMenuService.GetByGuid(MenuId);
            var vm = new AddNewOrderVM()
            {
                MenuId = MenuId,
                Item_Name = menu.Item_Name,
                Rate = menu.Rate,
                Quantity = 0

            };
            return PartialView("_AddMenu",model:vm);
        }

        public IActionResult AddNewOrder(AddNewOrderVM vm)
        {
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString("NewOrder") != null)
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString("NewOrder"));
            }

            HttpContext.Session.SetString("NewOrder", JsonConvert.SerializeObject(orderList));
            

            return PartialView("_NewOrder",model:orderList);
        }

        
    }
}
