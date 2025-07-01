
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class DepartmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> DepartmentList()
    {
        var entities = await _context.Departments.ToListAsync();
        return View(entities);
    }

    [HttpGet]
    public IActionResult CreateDepartment()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AfterCreateDepartment(Department entity)
    {
        if (!ModelState.IsValid)
        {
            return View("CreateDepartment", entity);
        }

        entity.CreatedDate = DateTime.UtcNow;
        entity.LastModifiedDate = DateTime.UtcNow;
        entity.CreatedBy = User.Identity?.Name ?? "System";
        entity.LastModifiedBy = User.Identity?.Name ?? "System";

        _context.Departments.Add(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsDepartment), new { id = entity.DepartmentCode });
    }

    public async Task<IActionResult> EditDepartment(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentCode == id);
        if (entity == null) return NotFound();

        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> AfterEditDepartment(string id, Department entity)
    {
        if (id != entity.DepartmentCode) return BadRequest();

        if (!ModelState.IsValid)
        {
            return View("EditDepartment", entity);
        }

        entity.LastModifiedDate = DateTime.UtcNow;
        entity.LastModifiedBy = User.Identity?.Name ?? "System";

        _context.Departments.Update(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsDepartment), new { id = entity.DepartmentCode });
    }

    public async Task<IActionResult> DeleteDepartment(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentCode == id);
        if (entity == null) return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("DeleteDepartment")]
    public async Task<IActionResult> DeleteDepartmentConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentCode == id);
        if (entity == null) return NotFound();

        _context.Departments.Remove(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DepartmentList));
    }

    public async Task<IActionResult> DetailsDepartment(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Departments.FirstOrDefaultAsync(e => e.DepartmentCode == id);
        if (entity == null) return NotFound();

        return View(entity);
    }
}
