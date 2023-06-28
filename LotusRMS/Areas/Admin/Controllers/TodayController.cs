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
using LotusRMS.Models;

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

        public async Task<IActionResult> Index()
        {
           
            var typeTable = new List<TodayTableVM>();
            var type =(await _ITableTypeService.GetAllAvailableAsync().ConfigureAwait(true)).Select(t => new TypeVM()
            {
                Id = t.Id,
                Type_Name = t.Type_Name
            });
            foreach (var item in type)
            {
               var tables =(await _ITableService.GetAllByTypeIdAsync(item.Id).ConfigureAwait(true))
                    .Select(async tab => await GetTodayDetailVM(tab)).ToList();

                typeTable.Add(new TodayTableVM()
                {
                    Type = item,
                    Table =(await Task.WhenAll(tables)).ToList()
                });

            }
            return View(typeTable);
        }
        private async Task<TodayDetailVM> GetTodayDetailVM(LotusRMS_Table tab)
        {
            var table = new TodayDetailVM()
            {
                Table_Id = tab.Id,
                Table_Name = tab.Table_Name,
                IsReserved = tab.IsReserved,
                Transaction = await GetAmount(tab.Id, tab.IsReserved).ConfigureAwait(true)
            };
            return table;
        }
        private async Task<double> GetAmount(Guid table_Id,bool isReserved)
        {
            if (!isReserved)
            {
                return 0;
            }
            else
            {
                var order = await _IOrderService.GetFirstOrDefaultByTableIdAsync(table_Id).ConfigureAwait(true);
                if (order != null)
                {
                    if (order.Order_Details != null && order.Order_Details.Count() > 0)
                    {
                        double total = order.Order_Details.Sum(x => x.GetTotal);
                        return total;
                    }
                }
                return 0;
                
            }

        }
    }
}
