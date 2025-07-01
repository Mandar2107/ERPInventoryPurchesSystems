
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> UserList()
    {
        var entities = await _context.Users
            .Include(u => u.Department)
            .Include(u => u.VendorProfile)
            .ToListAsync();
        return View(entities);
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
        ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AfterCreateUser(User entity)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
            return View("CreateUser", entity);
        }

        entity.CreatedDate = DateTime.UtcNow;
        entity.LastModifiedDate = DateTime.UtcNow;
        entity.CreatedBy = User.Identity.Name;
        entity.LastModifiedBy = User.Identity.Name;

        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsUser), new { id = entity.UserID });
    }

    public async Task<IActionResult> EditUser(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Users
            .Include(u => u.Department)
            .Include(u => u.VendorProfile)
            .FirstOrDefaultAsync(u => u.UserID == id);
        if (entity == null) return NotFound();

        ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", entity.DepartmentCode);
        ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", entity.VendorCode);
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> AfterEditUser(string id, User entity)
    {
        if (id != entity.UserID) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", entity.DepartmentCode);
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", entity.VendorCode);
            return View("EditUser", entity);
        }

        entity.LastModifiedDate = DateTime.UtcNow;
        entity.LastModifiedBy = User.Identity.Name;

        _context.Users.Update(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsUser), new { id = entity.UserID });
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Users
            .Include(u => u.Department)
            .Include(u => u.VendorProfile)
            .FirstOrDefaultAsync(u => u.UserID == id);
        if (entity == null) return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("DeleteUser")]
    public async Task<IActionResult> DeleteUserConfirmed(string id)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
        if (entity == null) return NotFound();

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(UserList));
    }

    public async Task<IActionResult> DetailsUser(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Users
            .Include(u => u.Department)
            .Include(u => u.VendorProfile)
            .FirstOrDefaultAsync(u => u.UserID == id);
        if (entity == null) return NotFound();

        return View(entity);
    }
}
