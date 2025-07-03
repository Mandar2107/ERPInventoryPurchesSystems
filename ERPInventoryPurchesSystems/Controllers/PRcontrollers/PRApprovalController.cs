using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class PRApprovalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PRApprovalController(ApplicationDbContext context)
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
    }

}
