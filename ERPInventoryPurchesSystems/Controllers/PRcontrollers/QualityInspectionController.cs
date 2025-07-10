using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class QualityInspectionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QualityInspectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var inspections = await _context.QualityInspections
                .Include(q => q.GRN)
                .Include(q => q.Department)
                .Include(q => q.InspectedBy)
                .ToListAsync();
            return View(inspections);
        }

        public IActionResult Create()
        {
            ViewBag.GRNs = new SelectList(_context.GoodsReceiptNotes, "GRNId", "GRNNumber");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName");

           
            ViewBag.Items = _context.Items
                .Select(i => new SelectListItem { Value = i.ItemCode, Text = i.ItemName })
                .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(QualityInspection inspection, List<InspectionItem> items)
        {
            inspection.InspectionDate = DateTime.Now;

            
            inspection.ActionTakenByUserId = inspection.InspectedByUserId;


            inspection.InspectionMethod = "Visual";


            _context.QualityInspections.Add(inspection);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                item.InspectionId = inspection.InspectionId;
                _context.InspectionItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var inspection = await _context.QualityInspections
                .Include(q => q.GRN)
                .Include(q => q.Department)
                .Include(q => q.InspectedBy)
                .Include(q => q.Items)
                .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(q => q.InspectionId == id);
            return View(inspection);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var inspection = await _context.QualityInspections.FindAsync(id);
            ViewBag.GRNs = new SelectList(_context.GoodsReceiptNotes, "GRNId", "GRNNumber", inspection.GRNId);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", inspection.DepartmentCode);
            ViewBag.Users = new SelectList(_context.Users, "UserID", "FullName", inspection.InspectedByUserId);
            return View(inspection);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(QualityInspection inspection)
        {
            _context.QualityInspections.Update(inspection);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var inspection = await _context.QualityInspections.FindAsync(id);
            return View(inspection);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inspection = await _context.QualityInspections.FindAsync(id);
            _context.QualityInspections.Remove(inspection);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
