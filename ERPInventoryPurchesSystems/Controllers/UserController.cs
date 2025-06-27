using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> UserList()
    {
        var users = await _context.Users.Include(u => u.Department).ToListAsync();
        return View(users);
    }

    public IActionResult CreateUser()
    {
        ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName");
            return View(user);
        }

        user.CreatedDate = DateTime.UtcNow;
        user.LastModifiedDate = DateTime.UtcNow;

        _context.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(UserList));
    }

    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", user.DepartmentCode);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(string id, User user)
    {
        if (id != user.UserID) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentCode", "DepartmentName", user.DepartmentCode);
            return View(user);
        }

        user.LastModifiedDate = DateTime.UtcNow;

        _context.Update(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(UserList));
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return View(user);
    }

    [HttpPost, ActionName("DeleteUser")]
    public async Task<IActionResult> DeleteUserConfirmed(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(UserList));
    }

    public async Task<IActionResult> DetailsUser(string id)
    {
        var user = await _context.Users.Include(u => u.Department).FirstOrDefaultAsync(u => u.UserID == id);
        if (user == null) return NotFound();
        return View(user);
    }
}
