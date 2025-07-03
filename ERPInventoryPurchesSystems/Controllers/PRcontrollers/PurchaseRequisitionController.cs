using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
  

    public class PurchaseRequisitionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRequisitionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PRlist()
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
        public async Task<IActionResult> Create(PurchesRequstiaon requisition)
        {
            requisition.PRNumber = GeneratePRNumber();
            requisition.CreatedDate = DateTime.Now;

            _context.PurchaseRequisitions.Add(requisition);
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

