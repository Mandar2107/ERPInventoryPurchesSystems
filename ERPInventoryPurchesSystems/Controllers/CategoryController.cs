using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERPInventoryPurchesSystems.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CategoryList()
        {
            var categories = await _context.Categories.Include(c => c.Department).ToListAsync();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CategoryList));
        }

        public async Task<IActionResult> EditCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", category.DepartmentCode);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(string id, Category category)
        {
            if (id != category.CategoryCode) return BadRequest();

            _context.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CategoryList));
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteCategoryConfirmed(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CategoryList));
        }

        public async Task<IActionResult> DetailsCategory(string id)
        {
            var category = await _context.Categories.Include(c => c.Department).FirstOrDefaultAsync(c => c.CategoryCode == id);
            if (category == null) return NotFound();
            return View(category);
        }
    }
}
