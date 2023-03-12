using LotusRMS.Models;
using LotusRMS.Models.Dto.OrderDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace LotusRMSweb.Areas.Order.Controllers
{
    [Area("Order")]
    [Authorize(Roles ="Admin,SuperAdmin,Waiter,Cashier,Bar")]
    public class HomeController : Controller
    {

        private readonly ITableTypeService _ITableTypeService;
        private readonly ITableService _ITableService;
        private readonly IMenuService _IMenuService;
        private readonly UserManager<RMSUser> _UserManager;
        private readonly IOrderService _IOrderService;
        public HomeController(IOrderService iOrderService,ITableTypeService TableTypeService, ITableService iTableService, IMenuService iMenuService,UserManager<RMSUser> userManager)
        {
            _IOrderService = iOrderService;
            _ITableTypeService = TableTypeService;
            _ITableService = iTableService;
            _IMenuService = iMenuService;
            _UserManager = userManager;
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
            var orders = _IOrderService.GetAll();
            var order = GetOrderVM(Id,""); 
            return PartialView("_Order",model:order
                );
        }
        public IActionResult Selectmenu(Guid MenuId,Guid TableId)
        {
            var menu=_IMenuService.GetByGuid(MenuId);
            var vm = new AddNewOrderVM()
            {
                MenuId = MenuId,
                TableId=TableId,
                Item_Name = menu.Item_Name,
                Rate = menu.Rate,
                Quantity = 0

            };
            
            return PartialView("_AddMenu",model:vm);
        }
        public OrderVm GetOrderVM(Guid? tableId,string? orderNo)
        {
            var OrderVM = new OrderVm()
            {

                Order_Details = new List<OrderDetailVm>()
            };

            if (tableId != Guid.Empty)
            {
                var order = _IOrderService.GetFirstOrDefaultByTableId((Guid)tableId);
                if (order != null)
                {
                    OrderVM = new OrderVm()
                    {
                        Id = order.Id,
                        TableId = order.Table_Id,
                        Date = order.DateTime,
                        Order_No = order.Order_No,
                        Order_Details = new List<OrderDetailVm>()
                    };
                    foreach(var item in order.Order_Details)
                    {
                        var menu = _IMenuService.GetFirstOrDefault(item.MenuId);
                        var orderDetail = new OrderDetailVm()
                        {
                            Id = item.Id,
                            MenuId = item.MenuId,
                            Item_Name = menu.Item_Name + " (" + menu.Menu_Unit.Unit_Symbol + " )",
                            Rate = item.Rate,
                            Quantity = item.Quantity,
                            IsComplete = item.IsComplete,
                            Total = item.GetTotal


                        };
                        OrderVM.Order_Details.Add(orderDetail);

                    }
                }


            } else if (orderNo != null)
            {
                var order = _IOrderService.GetFirstOrDefaultByOrderNo(orderNo);
                if (order != null)
                {
                    OrderVM = new OrderVm()
                    {
                        Id = order.Id,
                        TableId = order.Table_Id,
                        Date = order.DateTime,
                        Order_No = order.Order_No,
                        Order_Details = new List<OrderDetailVm>()
                    };
                    foreach (var item in order.Order_Details)
                    {

                        var menu = _IMenuService.GetFirstOrDefault(item.MenuId);
                        var orderDetail = new OrderDetailVm()
                        {
                            Id = item.Id,
                            MenuId = item.MenuId,
                            Item_Name = menu.Item_Name + " (" + menu.Menu_Unit.Unit_Symbol + " )",
                            Rate = item.Rate,
                            Quantity = item.Quantity,
                            IsComplete = item.IsComplete,
                            Total = item.GetTotal


                        };
                        OrderVM.Order_Details.Add(orderDetail);

                    }
                }

            }
            return OrderVM;

        }
       


        [HttpPost]
        public IActionResult AddNewOrder(AddNewOrderVM vm)
        {
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(vm.TableId.ToString()) != null)
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(vm.TableId.ToString()));
            }
            var menu=_IMenuService.GetAll().Where(x=>x.Id== vm.MenuId).FirstOrDefault();
            vm.Item_Name = menu.Item_Name +"("+menu.Menu_Unit.Unit_Symbol+")";
            vm.Total = vm.Quantity * vm.Rate;
            orderList.Add(vm);

            HttpContext.Session.SetString(vm.TableId.ToString(), JsonConvert.SerializeObject(orderList));


            return PartialView("_NewOrders",model:orderList);
        }
        [HttpPost]
        public IActionResult CompleteNewOrder(Guid tableId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(tableId.ToString()) != null)
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(tableId.ToString()));
            }

            var dto = new CreateOrderDTO()
            {
                Table_Id = tableId,
                UserId=userId,
                OrderDetails=new List<CreateOrderDetailDTO>()
            };
            foreach(var item in orderList)
            {
                var detailDto = new CreateOrderDetailDTO()
                {
                    Menu_Id = item.MenuId,
                    Quantity = item.Quantity,
                    Rate = item.Rate
                };
                dto.OrderDetails.Add(detailDto);
            }
            var id=_IOrderService.Create(dto);
            HttpContext.Session.SetString(tableId.ToString(), "");
            return View();
        }



    }
}
