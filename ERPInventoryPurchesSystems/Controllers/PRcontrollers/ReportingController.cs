using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class ReportingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Summary(DateTime? startDate, DateTime? endDate)
        {
            var data = await _context.PurchaseRequisitions
                .Where(pr => (!startDate.HasValue || pr.PRDate >= startDate) &&
                             (!endDate.HasValue || pr.PRDate <= endDate))
                .Include(pr => pr.Department)
                .ToListAsync();

            var summary = new
            {
                TotalPRs = data.Count,
                ApprovedPOs = await _context.PurchaseOrders.CountAsync(),
                TotalSpend = await _context.InvoiceItems.SumAsync(i => i.TotalAmount),
                AvgDeliveryTime = await _context.PurchaseOrders
                    .AverageAsync(po => EF.Functions.DateDiffDay(po.PODate, po.Items.FirstOrDefault().DeliveryDate)),
                PendingInvoices = await _context.Invoices.CountAsync(i => i.PaymentStatus == "Pending")
            };

            return View(summary);
        }

        public async Task<IActionResult> SpendByVendor()
        {
            var spendData = await _context.InvoiceItems
                .GroupBy(i => i.Invoice.Vendor.VendorName)
                .Select(g => new
                {
                    Vendor = g.Key,
                    TotalSpend = g.Sum(i => i.TotalAmount)
                }).ToListAsync();

            return View(spendData);
        }

        public async Task<IActionResult> MonthlyTrends()
        {
            var trends = await _context.PurchaseOrders
                .GroupBy(po => new { po.PODate.Year, po.PODate.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Month}/{g.Key.Year}",
                    TotalOrders = g.Count(),
                    TotalSpend = g.Sum(po => po.Items.Sum(i => i.TotalPrice))
                }).ToListAsync();

            return View(trends);
        }
    }

}
