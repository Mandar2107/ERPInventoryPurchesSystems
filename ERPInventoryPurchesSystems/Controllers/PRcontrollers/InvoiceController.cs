using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using Rotativa.AspNetCore;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Vendor)
                .Include(i => i.PurchaseOrder)
                .Include(i => i.GRN)
                .Include(i => i.Department)
                .Include(i => i.ProcessedBy)
                .ToListAsync();
            return View(invoices);
        }

        public IActionResult Create()
        {
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
            ViewBag.POs = new SelectList(_context.PurchaseOrders, "POId", "PONumber");
            ViewBag.GRNs = new SelectList(_context.GoodsReceiptNotes, "GRNId", "GRNNumber");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice, List<InvoiceItem> items)
        {
            invoice.InvoiceDate = DateTime.Now;
            invoice.TotalInvoiceAmount = items.Sum(i => i.TotalAmount);

       
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync(); 

           
            foreach (var item in items)
            {
                item.InvoiceId = invoice.InvoiceId; 
                item.MatchStatus = "Matched";       
                _context.InvoiceItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Vendor)
                .Include(i => i.PurchaseOrder)
                .Include(i => i.GRN)
                .Include(i => i.Department)
                .Include(i => i.ProcessedBy)
                .Include(i => i.Items)
                .ThenInclude(it => it.Item)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
            return View(invoice);
        }

        public IActionResult DownloadPdf(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Vendor)
                .Include(i => i.PurchaseOrder)
                .Include(i => i.GRN)
                .Include(i => i.Department)
                .Include(i => i.ProcessedBy)
                .Include(i => i.Items)
                .ThenInclude(it => it.Item)
                .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
                return NotFound();

            var document = new InvoicePdfDocument(invoice);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"Invoice_{invoice.InvoiceNumber}.pdf");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", invoice.VendorCode);
            ViewBag.POs = new SelectList(_context.PurchaseOrders, "POId", "PONumber", invoice.POId);
            ViewBag.GRNs = new SelectList(_context.GoodsReceiptNotes, "GRNId", "GRNNumber", invoice.GRNId);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", invoice.DepartmentCode);
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName", invoice.ProcessedByUserId);
            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            return View(invoice);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
