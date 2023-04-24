using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Table;
using LotusRMS.Models.Viewmodels.Today;
using LotusRMS.Models.Viewmodels.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using LotusRMS.Models.Viewmodels.MenuUnit;
using Newtonsoft.Json;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin,SuperAdmin,Cashier,Waiter,Bar")]
    public class TodayController : Controller
    {
        private readonly ITableTypeService _ITableTypeService;
        private readonly ITableService _ITableService;
        private readonly IOrderService _IOrderService;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        public TodayController(ITableTypeService iTableTypeService,
            ITableService iTableService,
            IOrderService iOrderService,
          Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _ITableTypeService = iTableTypeService;
            _ITableService = iTableService;
            _IOrderService = iOrderService;
            _env = env;
        }

        public IActionResult Index()
        {
           
            var typeTable = new List<TodayTableVM>();
            var type = _ITableTypeService.GetAllAvailable().Select(t => new TypeVM()
            {
                Id = t.Id,
                Type_Name = t.Type_Name
            });
            foreach (var item in type)
            {
                var tables = _ITableService.GetAllByTypeId(item.Id).Select(tab => new TodayDetailVM()
                {
                    Table_Id = tab.Id,
                    Table_Name = tab.Table_Name,
                    IsReserved = tab.IsReserved,
                    Transaction=GetAmount(tab.Id,tab.IsReserved)
                }).ToList();

                typeTable.Add(new TodayTableVM()
                {
                    Type = item,
                    Table = tables
                });

            }
            return View(typeTable);
        }
        private double GetAmount(Guid table_Id,bool isReserved)
        {
            if (!isReserved)
            {
                return 0;
            }
            else
            {
                var order = _IOrderService.GetFirstOrDefaultByTableId(table_Id);
                double total = order.Order_Details.Sum(x => x.GetTotal);
                return total;
            }

        }
    }
}
