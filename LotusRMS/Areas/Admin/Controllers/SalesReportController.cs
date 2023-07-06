
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Report;
using LotusRMS.Utility.Enum;
using Microsoft.AspNetCore.Identity;
using LotusRMS.Models;
using System.Web.WebPages;
using System.Linq.Expressions;
using LotusRMS.Models.Viewmodels.SalesReport;
using MimeKit.Tnef;

namespace LotusRMSweb.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SalesReportController : Controller
    {
        private readonly IMenuService _iMenuService;
        private readonly UserManager<RMSUser> _iUserManager;
        private readonly ICustomerService _iCustomerService;
        private readonly ITableService _iTableService;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly ICheckoutService _iCheckoutService;
        public SalesReportController(IMenuService iMenuService,
            ICustomerService iCustomerService,
            ITableService iTableService,
            UserManager<RMSUser> iUserManager,
            ICheckoutService iCheckoutService,
            RoleManager<IdentityRole> roleManager)
        {
            _iMenuService = iMenuService;
            _iCustomerService = iCustomerService;
            _iTableService = iTableService;
            _iUserManager = iUserManager;
            _iCheckoutService = iCheckoutService;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetReportType(string reportType) {
           var list= new List<SelectList>();

            if (reportType == SalesReportType.ReportType.item.ToString())
            {
                list = await GetItemList().ConfigureAwait(true);
            }else if (reportType == SalesReportType.ReportType.table.ToString())
            {
                list = await GetTableList().ConfigureAwait(true);
            }else if (reportType == SalesReportType.ReportType.customer.ToString())
            {
                list = await GetCustomerList().ConfigureAwait(true);
            }else if (reportType == SalesReportType.ReportType.user.ToString())
            {
                list = GetUserList();
            }
            return Ok(list);
        
        }
        public async Task<List<SelectList>> GetItemList() {
            var menuList = (await _iMenuService.GetAllAvailableAsync().ConfigureAwait(true)).Select(menu => new SelectList(){ 
            Id= menu.Id.ToString(),
            Name=menu.Item_Name }).ToList();
            menuList.Insert(0, new SelectList() { Id = Guid.Empty.ToString(), Name = "All" });
            return menuList;
        } 
        public async Task<List<SelectList>> GetCustomerList() {
            var customerList = (await _iCustomerService.GetAllAvailableAsync().ConfigureAwait(true)).Select(customer => new SelectList(){ 
            Id= customer.Id.ToString(),
            Name= customer.Name }).ToList();
            customerList.Insert(0, new SelectList() { Id = Guid.Empty.ToString(), Name = "Cash" });
            return customerList;
        }
        public async Task<List<SelectList>> GetTableList() {
            var tableList = (await _iTableService.GetAllAvailableAsync()).Select(table => new SelectList(){ 
            Id= table.Id.ToString(),
            Name= table.Table_Name }).ToList();
            tableList.Insert(0, new SelectList() { Id = Guid.Empty.ToString(), Name = "All" });
            return tableList;        
        }   
        public List<SelectList> GetUserList() {

            var users = _iUserManager.Users;
            var roles = roleManager.Roles.Where(x => x.Name != "SuperAdmin");
            var userList = new List<SelectList>();
            
            foreach (var role in roles)
            {
                var userAtRole = _iUserManager.GetUsersInRoleAsync(role.Name).Result.Select(u => new SelectList()
                {
                    Id = u.Id,
                    Name = u.FirstName +" "+u.MiddleName+" "+u.LastName
                });
                userList.AddRange(userAtRole);
            }
            userList.Insert(0, new SelectList() { Id = Guid.Empty.ToString(), Name = "All" });
            return userList;
        
        }
        public IActionResult GetReport()
        {
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder(DateTime startDate,DateTime endDate,string ReportType,string Id)
        {
            endDate = endDate.AddDays(1).AddMilliseconds(-1);
            var gId = Guid.Empty;
            if (Id != "0")
            {
                gId = Guid.Parse(Id);
            }
            var checkout = await _iCheckoutService.GetAllByDateRangeAsync( startDate,endDate);
            if(checkout != null)
            {
                if(ReportType == SalesReportType.ReportType.table.ToString())
                {
                    if (gId != Guid.Empty)
                    {
                        checkout = checkout.Where(x => x.Order.Table_Id == gId);
                        var table = await _iTableService.GetFirstOrDefaultByIdAsync(gId).ConfigureAwait(true);
                        var salesReport = new SalesReportBySingleTable()
                        {
                            DateFrom=startDate,
                            DateTo=endDate,
                            Table_Name=table.Table_Name,
                            Table_Type=table.Table_Type.Type_Name,
                            Id=table.Id
                        };
                            return await GetReportBySingleTable(checkout, salesReport).ConfigureAwait(true);
                    }
                    else
                    {
                        return await GetReportByAllTable(checkout).ConfigureAwait(true);
                    }
                }
                else if (ReportType == SalesReportType.ReportType.customer.ToString())
                {
                    if (gId != Guid.Empty)
                    {
                        checkout = checkout.Where(x => x.Customer_Id == gId);
                    }
                } else if (ReportType == SalesReportType.ReportType.user.ToString())
                {
                    if (gId != Guid.Empty)
                    {
                        checkout = checkout.Where(x => x.Order.OrderBy == gId.ToString());
                    }
                }
            }
            return Ok(checkout);
        }

        public async Task<IActionResult> GetReportByAllTable(IEnumerable<LotusRMS_Checkout> checkout )
        {
            
            var tables = await _iTableService.GetAllAsync().ConfigureAwait(true);
            var salesReport = new List<SalesReportByAllTable>();
            if (checkout != null)
            {
                foreach(var table in tables)
                {
                    var checkoutForTable = checkout.Where(x => x.Order.Table_Id == table.Id);
                    var saleReport = new SalesReportByAllTable()
                    {
                        Id= table.Id,
                        TableType=table.Table_Type.Type_Name,                        
                        TableName = table.Table_Name,
                        Total = checkoutForTable.Sum(x => x.Total),
                        Discount = checkoutForTable.Sum(x => x.Discount)
                    };
                    salesReport.Add(saleReport);
                }
                
            }
            return PartialView("_ReportByAllTable",salesReport);


        }

        public async Task<IActionResult> GetReportBySingleTable(IEnumerable<LotusRMS_Checkout> checkout,SalesReportBySingleTable salesReport)
        {
           

            return PartialView("_ReportBySingleTable");
        }

}
}
