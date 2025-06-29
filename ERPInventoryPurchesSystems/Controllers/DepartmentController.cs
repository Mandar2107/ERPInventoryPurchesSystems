using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DepartmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> DepartmentList()
    {
        return View(await _context.Departments.ToListAsync());
    }

    public IActionResult CreateDepartment() => View();

    [HttpPost]
    public async Task<IActionResult> AfterCreateDepartment(Department department)
    {
        if (!ModelState.IsValid) return View(department);

        department.CreatedDate = DateTime.UtcNow;
        department.LastModifiedDate = DateTime.UtcNow;

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
    public async Task<IActionResult> AfterEditDepartment(string id, Department department)
    {
        

        department.LastModifiedDate = DateTime.UtcNow;

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
