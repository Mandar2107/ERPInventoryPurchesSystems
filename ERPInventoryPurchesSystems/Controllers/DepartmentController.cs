using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;

namespace ERPInventoryPurchesSystems.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DepartmentList()
        {
            var departments = await _context.Departments.ToListAsync();
            return View(departments);
        }

        public IActionResult CreateDepartment() => View();

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DepartmentList));
        }

        public async Task<IActionResult> EditDepartment(string id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(string id, Department department)
        {
            if (id != department.DepartmentCode) return BadRequest();

            _context.Update(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DepartmentList));
        }

        public async Task<IActionResult> DeleteDepartment(string id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartmentConfirmed(string id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DepartmentList));
        }

        public async Task<IActionResult> DetailsDepartment(string id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return View(department);
        }
    }
}
