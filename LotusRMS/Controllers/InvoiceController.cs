using LotusRMS.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ICompanyService _iCompanyService;

        public InvoiceController(IInvoiceService invoiceService, ICompanyService iCompanyService)
        {
            _invoiceService = invoiceService;
            _iCompanyService = iCompanyService;
        }

        public IActionResult InvoicePrint(Guid Id)
        {
            var invoice = _invoiceService.GetFirstOrDefault(Id);
            ViewBag.Company = _iCompanyService.GetCompany();

            return View(invoice);
        }
    }
}
