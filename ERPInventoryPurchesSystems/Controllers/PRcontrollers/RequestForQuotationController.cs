using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class RequestForQuotationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestForQuotationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rfqs = await _context.RequestForQuotations
                .Include(r => r.PurchaseRequisition)
                .Include(r => r.Vendor)
                .ToListAsync();
            return View(rfqs);
        }


        public IActionResult Create()
        {
            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber");
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RequestForQuotation rfq, List<RFQItem> items)
        {
            _context.RequestForQuotations.Add(rfq);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                item.RFQId = rfq.RFQId;
                _context.RFQItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var rfq = await _context.RequestForQuotations
                .Include(r => r.PurchaseRequisition)
                .Include(r => r.Vendor)
                .Include(r => r.Items)
                .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(r => r.RFQId == id);
            return View(rfq);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rfq = await _context.RequestForQuotations.FindAsync(id);
            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber", rfq.PRId);
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", rfq.VendorCode);
            return View(rfq);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RequestForQuotation rfq)
        {
            _context.RequestForQuotations.Update(rfq);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var rfq = await _context.RequestForQuotations.FindAsync(id);
            return View(rfq);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rfq = await _context.RequestForQuotations.FindAsync(id);
            _context.RequestForQuotations.Remove(rfq);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> GetPRItems(int prId)
        {
            var items = await _context.PRItems
                .Where(i => i.PurchaseRequisitionID == prId)
                .Include(i => i.Item)
                .Select(i => new {
                    ItemCode = i.ItemCode,
                    ItemName = i.Item.ItemName,
                    Quantity = i.Quantity,
                    RequiredDate = i.RequiredDate,
                    Justification = i.Justification
                })
                .ToListAsync();

            return Json(items);
        }


    }

}
