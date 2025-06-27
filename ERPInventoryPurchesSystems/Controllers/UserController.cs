using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;

namespace ERPInventoryPurchesSystems.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> UserList()
        {
            return View(await _context.Users.ToListAsync());
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
      
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserList));
            }
            return View(user);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
  
        public async Task<IActionResult> EditUser(string id, User user)
        {
            if (id != user.UserID) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Users.Any(e => e.UserID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(UserList));
            }

            return View(user); // This should be inside the method, not outside
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
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }
    }
}
