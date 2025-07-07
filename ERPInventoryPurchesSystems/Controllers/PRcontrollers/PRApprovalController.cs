using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class ApprovalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApprovalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var approvals = await _context.Approvals
                .Include(a => a.PurchaseRequisition)
                .Include(a => a.Approver)
                .Include(a => a.Department)
                .ToListAsync();
            return View(approvals);
        }

        public IActionResult Create()
        {
            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber");
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Approval approval)
        {
            approval.ApprovalDate = DateTime.Now;
            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var approval = await _context.Approvals
                .Include(a => a.PurchaseRequisition)
                .Include(a => a.Approver)
                .Include(a => a.Department)
                .FirstOrDefaultAsync(a => a.ApprovalID == id);
            return View(approval);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var approval = await _context.Approvals.FindAsync(id);
            ViewBag.PRs = new SelectList(_context.PurchaseRequisitions, "PurchaseRequisitionID", "PRNumber", approval.PurchaseRequisitionID);
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName", approval.ApproverUserID);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", approval.DepartmentCode);
            return View(approval);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Approval approval)
        {
            _context.Approvals.Update(approval);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var approval = await _context.Approvals.FindAsync(id);
            return View(approval);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var approval = await _context.Approvals.FindAsync(id);
            _context.Approvals.Remove(approval);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
