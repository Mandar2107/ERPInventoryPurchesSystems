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
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseOrder order, List<POItem> items)
        {
            order.PODate = DateTime.Now;
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

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.PurchaseOrders
                .Include(p => p.PurchaseRequisition)
                .Include(p => p.Vendor)
                .Include(p => p.Department)
                .Include(p => p.RequestedBy)
                .Include(p => p.Items)
                .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(p => p.POId == id);
            return View(order);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber", order.PRId);
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", order.VendorCode);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", order.DepartmentCode);
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName", order.RequestedByUserId);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PurchaseOrder order)
        {
            _context.PurchaseOrders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            _context.PurchaseOrders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
