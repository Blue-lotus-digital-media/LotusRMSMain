using LotusRMS.Models.Service;
using LotusRMS.Utility;
using LotusRMS.Utility.Enum;
using Microsoft.AspNetCore.Mvc;
using static LotusRMS.Utility.Enum.Enums;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IOrderService _iOrderService;

        public HomeController(IOrderService iOrderService)
        {
            _iOrderService = iOrderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UnitTest()
        {
            return View();
        }
        #region APICalls
        public IActionResult GetTableBooked(ReportType type)
        {
            var today = CurrentTime.DateTimeToday();
            var startDate = Convert.ToDateTime(today);
            var endDate = CurrentTime.DateTimeNow();
            if (type == ReportType.Week)
            {
                startDate = startDate.AddDays(-7);
            }else if (type == ReportType.Month)
            {
                startDate = startDate.AddDays(-startDate.Day+1);
            }else if (type == ReportType.Year)
            {
                startDate = startDate.AddMonths(-startDate.Month+1).AddDays(-startDate.Day+1);
            }
            var tableBookedCount = _iOrderService.GetAllByDateRange(startDate, endDate);

            return Json(new {data=tableBookedCount.Count()});
        }
       



        #endregion

    }
}