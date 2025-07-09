
using ERPInventoryPurchesSystems.Models.Reporting;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, string departmentCode, string vendorCode, string itemCategory, List<string> statusFilter)
        {
            var model = new ReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                DepartmentCode = departmentCode,
                VendorCode = vendorCode,
                ItemCategory = itemCategory,
                StatusFilter = statusFilter,

                TotalPRs = await _context.PurchaseRequisitions
                    .Where(pr => (!startDate.HasValue || pr.PRDate >= startDate) &&
                                 (!endDate.HasValue || pr.PRDate <= endDate))
                    .CountAsync(),

                ApprovedPOs = await _context.PurchaseOrders.CountAsync(),

                TotalSpend = await _context.InvoiceItems.SumAsync(i => i.TotalAmount),

                AverageDeliveryTime = await _context.PurchaseOrders
                    .Where(po => po.Items.Any())
                    .AverageAsync(po => EF.Functions.DateDiffDay(po.PODate, po.Items.FirstOrDefault().DeliveryDate)),

                VendorPerformanceScore = 92.5, // Placeholder, calculate based on your logic

                PendingInvoices = await _context.Invoices.CountAsync(i => i.PaymentStatus == "Pending"),

                VendorNames = await _context.InvoiceItems
                    .GroupBy(i => i.Invoice.Vendor.VendorName)
                    .Select(g => g.Key)
                    .ToListAsync(),

                VendorSpend = await _context.InvoiceItems
                    .GroupBy(i => i.Invoice.Vendor.VendorName)
                    .Select(g => g.Sum(i => i.TotalAmount))
                    .ToListAsync(),

                Months = await _context.PurchaseOrders
                    .GroupBy(po => new { po.PODate.Year, po.PODate.Month })
                    .Select(g => $"{g.Key.Month}/{g.Key.Year}")
                    .ToListAsync(),

                MonthlySpend = await _context.PurchaseOrders
                    .GroupBy(po => new { po.PODate.Year, po.PODate.Month })
                    .Select(g => g.Sum(po => po.Items.Sum(i => i.TotalPrice)))
                    .ToListAsync(),
                Categories = await _context.InvoiceItems
    .Select(i => i.Item.Category.CategoryName) // or .CategoryCode
    .Distinct()
    .ToListAsync(),

                CategorySpend = await _context.InvoiceItems
                    .GroupBy(i => i.Item.Category)
                    .Select(g => g.Sum(i => i.TotalAmount))
                    .ToListAsync()
            };

            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "DepartmentCode", "DepartmentName");
            ViewBag.Vendors = new SelectList(await _context.Vendors.ToListAsync(), "VendorCode", "VendorName");
            ViewBag.Categories = new SelectList(await _context.Items.Select(i => i.Category).Distinct().ToListAsync());

            return View(model);
        }

        public IActionResult ExportToExcel()
        {
            // TODO: Implement Excel export logic
            return Content("Excel export not implemented yet.");
        }

        public IActionResult ExportToPdf()
        {
            // TODO: Implement PDF export logic
            return Content("PDF export not implemented yet.");
        }

        public IActionResult ScheduleReport()
        {
            // TODO: Implement scheduling logic
            return Content("Report scheduling not implemented yet.");
        }

        public IActionResult EmailSummary()
        {
            // TODO: Implement email logic
            return Content("Email summary not implemented yet.");
        }
    }
}
