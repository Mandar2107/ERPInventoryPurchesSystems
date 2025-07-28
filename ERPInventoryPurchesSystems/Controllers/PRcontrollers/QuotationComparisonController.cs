using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class QuotationComparisonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotationComparisonController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index()
        {
            var comparisons = await _context.QuotationComparisons
                .Include(q => q.PurchaseRequisition)
                .Include(q => q.Department)
                .Include(q => q.RequestedBy)
                .ToListAsync();

            return View(comparisons);
        }

       
        public async Task<IActionResult> Index1(int id)
        {
            var comparison = await _context.QuotationComparisons
                .Include(q => q.Items)
                    .ThenInclude(i => i.Item)
                .Include(q => q.Items)
                    .ThenInclude(i => i.Vendor)
                .FirstOrDefaultAsync(q => q.ComparisonId == id);

            return View("Index1", comparison);
        }

        [HttpPost]
        public async Task<IActionResult> Selection(int ComparisonId, Dictionary<string, string> SelectedVendor)
        {
            foreach (var entry in SelectedVendor)
            {
                string itemCode = entry.Key;
                string vendorCode = entry.Value;

                var selectedItem = await _context.QuotationComparisonItems
                    .FirstOrDefaultAsync(i => i.ComparisonId == ComparisonId && i.ItemCode == itemCode && i.VendorCode == vendorCode);

                if (selectedItem != null)
                {
                    selectedItem.IsSelected = true;
                    _context.QuotationComparisonItems.Update(selectedItem);
                }
            }

            await _context.SaveChangesAsync();

            
            var prId = await _context.QuotationComparisons
                .Where(q => q.ComparisonId == ComparisonId)
                .Select(q => q.PRId)
                .FirstOrDefaultAsync();

            return RedirectToAction("Create", "PurchaseOrder", new { prId });
        }


        // Show form to create a new comparison
        public IActionResult Create()
        {
            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");

            return View();
        }

        // Save new comparison and items
        [HttpPost]
        public async Task<IActionResult> Create(QuotationComparison comparison, List<QuotationComparisonItem> items)
        {
            comparison.DateOfComparison = DateTime.Now;

            _context.QuotationComparisons.Add(comparison);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                item.ComparisonId = comparison.ComparisonId;
                item.IsSelected = false;
                _context.QuotationComparisonItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // View details of a comparison
        public async Task<IActionResult> Details(int id)
        {
            var comparison = await _context.QuotationComparisons
                .Include(q => q.PurchaseRequisition)
                .Include(q => q.Department)
                .Include(q => q.RequestedBy)
                .Include(q => q.Items)
                    .ThenInclude(i => i.Item)
                .Include(q => q.Items)
                    .ThenInclude(i => i.Vendor)
                .FirstOrDefaultAsync(q => q.ComparisonId == id);

            return View(comparison);
        }

        // Edit comparison
        public async Task<IActionResult> Edit(int id)
        {
            var comparison = await _context.QuotationComparisons.FindAsync(id);

            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber", comparison.PRId);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", comparison.DepartmentCode);
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName", comparison.RequestedByUserId);

            return View(comparison);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(QuotationComparison comparison)
        {
            _context.QuotationComparisons.Update(comparison);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Delete comparison
        public async Task<IActionResult> Delete(int id)
        {
            var comparison = await _context.QuotationComparisons.FindAsync(id);
            return View(comparison);
        }

        // ✅ AJAX endpoint for PR-related items
        [HttpGet]
        [Route("QuotationComparison/GetItemsByPR")]
        public JsonResult GetItemsByPR(int prId)
        {
            var items = _context.PRItems
                .Where(pi => pi.PurchaseRequisitionID == prId)
                .Include(pi => pi.Item)
                .Select(pi => new {
                    Value = pi.Item.ItemCode,
                    Text = pi.Item.ItemName
                }).Distinct().ToList();

            return Json(items);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comparison = await _context.QuotationComparisons.FindAsync(id);
            _context.QuotationComparisons.Remove(comparison);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
