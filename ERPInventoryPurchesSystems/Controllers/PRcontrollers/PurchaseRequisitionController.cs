using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class PurchaseRequisitionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRequisitionController(ApplicationDbContext context)
        {
            _context = context;
        }

 
        public async Task<IActionResult> PRList()
        {
            var requisitions = await _context.PurchaseRequisitions
                .Include(p => p.Department)
                .Include(p => p.SubmittedBy)
                .ToListAsync();
            return View(requisitions);
        }

        
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");
            ViewBag.Items = new SelectList(_context.Items, "ItemCode", "ItemName");
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(PurchesRequstiaon requisition, List<PRItem> Items)
        {
            requisition.PRNumber = GeneratePRNumber();
            requisition.CreatedDate = DateTime.Now;
            requisition.CreatedBy = "System";
            requisition.Status = "Pending";

            _context.PurchaseRequisitions.Add(requisition);
            await _context.SaveChangesAsync();

            foreach (var item in Items)
            {
                item.PurchaseRequisitionID = requisition.PurchaseRequisitionID;
                _context.PRItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // View PR Details
        public async Task<IActionResult> Details(int id)
        {
            var pr = await _context.PurchaseRequisitions
                .Include(p => p.Department)
                .Include(p => p.SubmittedBy)
                .Include(p => p.Items)
                .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(p => p.PurchaseRequisitionID == id);

            return View(pr);
        }

        // Show Edit Form
        public async Task<IActionResult> Edit(int id)
        {
            var pr = await _context.PurchaseRequisitions.FindAsync(id);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", pr.DepartmentCode);
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName", pr.SubmittedByUserID);
            return View(pr);
        }

        // Handle Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(PurchesRequstiaon requisition)
        {
            _context.PurchaseRequisitions.Update(requisition);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Show Delete Confirmation
        public async Task<IActionResult> Delete(int id)
        {
            var pr = await _context.PurchaseRequisitions.FindAsync(id);
            return View(pr);
        }

        // Handle Delete POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pr = await _context.PurchaseRequisitions.FindAsync(id);
            _context.PurchaseRequisitions.Remove(pr);
            await _context.SaveChangesAsync();
            return RedirectToAction("PRList");
        }

        private string GeneratePRNumber()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"PR-{timestamp}-{random}";
        }
    }
}
