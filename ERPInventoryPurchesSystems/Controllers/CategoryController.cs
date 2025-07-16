using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> CategoryList()
    {
        var categories = await _context.Categories
            .Include(c => c.Department)
            .ToListAsync();

        return View(categories);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        var departments = _context.Departments.ToList();
        ViewBag.Departments = new SelectList(departments, "DepartmentCode", "DepartmentName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AfterCreateCategory(Category category )
    {
        

        category.CreatedDate = DateTime.UtcNow;
        category.LastModifiedDate = DateTime.UtcNow;
        category.CreatedBy = "System";
        category.LastModifiedBy = "System";

        _context.Categories.Add(category);
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
    public async Task<IActionResult> AfterEditCategory(string id, Category category)
    {


        //if (!ModelState.IsValid)
        //{
        //    ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", category.DepartmentCode);
        //    return View("EditCategory", category);
        //}
        category.CreatedDate = DateTime.UtcNow;
        category.LastModifiedDate = DateTime.UtcNow;
        category.CreatedBy = "System";
        category.LastModifiedBy = "System";

        category.AssociatedGLAccounts = "agashhss";

        _context.Categories.Update(category);
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
        var category = await _context.Categories
            .Include(c => c.Department)
            .Include(c => c.Items) 
            .FirstOrDefaultAsync(c => c.CategoryCode == id);

        if (category == null) return NotFound();

        return View(category);
    }


    public IActionResult TestDepartments()
    {
        var departments = _context.Departments.ToList();
        return Content("Departments count: " + departments.Count);
    }
}
