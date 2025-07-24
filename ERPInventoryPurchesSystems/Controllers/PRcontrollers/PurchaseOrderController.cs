using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.PurchaseOrders
                .Include(p => p.PurchaseRequisition)
                .Include(p => p.Vendor)
                .Include(p => p.Department)
                .Include(p => p.RequestedBy)
                .ToListAsync();
            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber");
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseOrder order, List<POItem> items)
        {
            order.PODate = DateTime.Now;
            order.PONumber = $"PO-{DateTime.Now:yyyyMMddHHmmss}";
            _context.PurchaseOrders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                item.POId = order.POId;
                _context.POItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetPRDetails(int prId)
        {
            var pr = await _context.PurchaseRequisitions
                .Include(p => p.Department)
                .Include(p => p.SubmittedBy)
                .FirstOrDefaultAsync(p => p.PurchaseRequisitionID == prId);

            if (pr == null)
                return NotFound();

            // Get selected vendors from quotation comparison
            var selectedItems = await _context.QuotationComparisonItems
                .Include(q => q.Item)
                .Include(q => q.Vendor)
                .Include(q => q.Comparison)
                .Where(q => q.IsSelected && q.Comparison.PRId == prId)
                .ToListAsync();

            var firstVendor = selectedItems.Select(i => i.Vendor).FirstOrDefault();

            var groupedItems = selectedItems
                .GroupBy(i => i.ItemCode)
                .Select(g => new {
                    itemCode = g.Key,
                    itemName = g.First().Item.ItemName,
                    quantity = g.Sum(x => x.QuotedQuantity),
                    unitPrice = g.First().UnitPrice
                });

            var result = new
            {
                vendorCode = firstVendor?.VendorCode,
                vendorName = firstVendor?.VendorName,
                vendorContact = firstVendor?.ContactPersonName,
                departmentCode = pr.DepartmentCode,
                requestedByUserId = pr.SubmittedByUserID,
                requestedByName = pr.SubmittedBy.FullName,
                items = groupedItems
            };

            return Json(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            _context.PurchaseOrders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
