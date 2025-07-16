using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class GoodsReceiptNoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoodsReceiptNoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var grns = await _context.GoodsReceiptNotes
                .Include(g => g.PurchaseOrder)
                .Include(g => g.Vendor)
                .Include(g => g.Department)
                .Include(g => g.ReceivedBy)
                .ToListAsync();

            return View(grns);
        }

        public IActionResult Create()
        {
            ViewBag.POs = new SelectList(_context.PurchaseOrders, "POId", "PONumber");
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");

            // For stock display
            ViewBag.ItemsWithStock = _context.Items
                .Select(i => new { i.ItemCode, i.ItemName, i.QuantityInHand })
                .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GoodsReceiptNote grn, List<GRNItem> items)
        {
            grn.GRNDate = DateTime.Now;
            grn.GRNNumber = "GRN-" + DateTime.Now.Ticks;
            grn.DeliveryNoteNumber = "DN-" + DateTime.Now.Ticks;

            var validItemCodes = await _context.Items.Select(i => i.ItemCode).ToListAsync();
            var invalidItems = items.Where(i => string.IsNullOrEmpty(i.ItemCode) || !validItemCodes.Contains(i.ItemCode)).ToList();

            if (invalidItems.Any())
            {
                ModelState.AddModelError("", "One or more selected items are invalid.");
                ViewBag.POs = new SelectList(_context.PurchaseOrders, "POId", "PONumber");
                ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
                ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
                ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");
                ViewBag.ItemsWithStock = _context.Items
                    .Select(i => new { i.ItemCode, i.ItemName, i.QuantityInHand })
                    .ToList();
                return View(grn);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.GoodsReceiptNotes.Add(grn);
                await _context.SaveChangesAsync();

                foreach (var item in items)
                {
                    item.GRNId = grn.GRNId;
                    _context.GRNItems.Add(item);

                    var stockItem = await _context.Items.FirstOrDefaultAsync(i => i.ItemCode == item.ItemCode);
                    if (stockItem != null)
                    {
                        stockItem.QuantityInHand += item.ReceivedQuantity;
                        _context.Items.Update(stockItem);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return RedirectToAction("Index");
        }

        // ✅ AJAX endpoint for autofill
        [HttpGet]
        public async Task<IActionResult> GetPOData(int poId)
        {
            var po = await _context.PurchaseOrders
                .Include(p => p.Vendor)
                .Include(p => p.Department)
                .Include(p => p.Items)
                    .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(p => p.POId == poId);

            if (po == null) return NotFound();

            var result = new
            {
                vendorCode = po.VendorCode,
                vendorName = po.Vendor?.VendorName,
                departmentCode = po.DepartmentCode,
                departmentName = po.Department?.DepartmentName,
                items = po.Items.Select(i => new
                {
                    itemCode = i.ItemCode,
                    itemName = i.Item.ItemName,
                    orderedQuantity = i.Quantity,
                    unit = i.Item.UOM
                })
            };

            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var grn = await _context.GoodsReceiptNotes
                .Include(g => g.PurchaseOrder)
                .Include(g => g.Vendor)
                .Include(g => g.Department)
                .Include(g => g.ReceivedBy)
                .Include(g => g.Items)
                    .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(g => g.GRNId == id);

            return View(grn);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var grn = await _context.GoodsReceiptNotes.FindAsync(id);
            ViewBag.POs = new SelectList(_context.PurchaseOrders, "POId", "PONumber", grn.POId);
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", grn.VendorCode);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", grn.DepartmentCode);
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName", grn.ReceivedByUserId);
            return View(grn);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GoodsReceiptNote grn)
        {
            _context.GoodsReceiptNotes.Update(grn);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grn = await _context.GoodsReceiptNotes.FindAsync(id);
            return View(grn);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grn = await _context.GoodsReceiptNotes.FindAsync(id);
            _context.GoodsReceiptNotes.Remove(grn);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
