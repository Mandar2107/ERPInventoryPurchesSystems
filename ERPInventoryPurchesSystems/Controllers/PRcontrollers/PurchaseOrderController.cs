
using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: List all purchase orders
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

        // GET: Create PO manually
        [HttpGet]
        public IActionResult Create()
        {
            var model = new PurchaseOrder
            {
                Items = new List<POItem>(),
                PODate = DateTime.Now,
                PONumber = $"PO-{DateTime.Now:yyyyMMddHHmmss}"
            };

            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber");
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");
            ViewBag.RequestedByName = "";
            ViewBag.IsReadOnly = false;

            return View(model);
        }

        // GET: Create PO from selected PR (quotation comparison)
        [HttpGet]
        [Route("PurchaseOrder/CreateFromPR/{prId}")]
        public async Task<IActionResult> CreateFromPR(int prId)
        {
            var pr = await _context.PurchaseRequisitions
                .Include(p => p.Department)
                .Include(p => p.SubmittedBy)
                .FirstOrDefaultAsync(p => p.PurchaseRequisitionID == prId);

            var selectedItems = await _context.QuotationComparisonItems
                .Include(q => q.Item)
                .Include(q => q.Vendor)
                .Include(q => q.Comparison)
                .Where(q => q.IsSelected && q.Comparison.PRId == prId)
                .ToListAsync();

            var model = new PurchaseOrder
            {
                PRId = prId,
                DepartmentCode = pr?.DepartmentCode,
                RequestedByUserId = pr?.SubmittedByUserID,
                VendorCode = selectedItems.FirstOrDefault()?.VendorCode,
                VendorContact = selectedItems.FirstOrDefault()?.Vendor?.ContactPersonName,
                Items = selectedItems.Select(i => new POItem
                {
                    ItemCode = i.ItemCode,
                    Quantity = i.QuotedQuantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.QuotedQuantity * i.UnitPrice,
                    DeliveryDate = DateTime.Today.AddDays(7),
                    DeliveryTerms = "Standard",
                    VendorCode = i.VendorCode
                }).ToList()
            };

            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber", prId);
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");
            ViewBag.RequestedByName = pr?.SubmittedBy?.FullName;
            ViewBag.IsReadOnly = true;

            return View("Create", model);
        }

        // POST: Save PO
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

        // GET: AJAX - Get PR details for dynamic form
        [HttpGet]
        public async Task<IActionResult> GetPRDetails(int prId)
        {
            var pr = await _context.PurchaseRequisitions
                .Include(p => p.Department)
                .Include(p => p.SubmittedBy)
                .FirstOrDefaultAsync(p => p.PurchaseRequisitionID == prId);

            if (pr == null)
                return NotFound();

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
                    unitPrice = g.First().UnitPrice,
                    vendorCode = g.First().VendorCode,
                    vendorName = g.First().Vendor.VendorName
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

        // GET: Confirm delete
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            return View(order);
        }

        // POST: Delete confirmed
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
