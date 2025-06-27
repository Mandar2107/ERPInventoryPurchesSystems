using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;

namespace ERPInventoryPurchesSystems.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        public IActionResult CreateItem()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> CreateItem(Item item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedBy = User.Identity?.Name ?? "System";
                item.CreatedDate = DateTime.UtcNow;
                item.LastModifiedBy = item.CreatedBy;
                item.LastModifiedDate = item.CreatedDate;

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }


        public async Task<IActionResult> EditItem(string id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
       
        public async Task<IActionResult> EditItem(string id, Item item)
        {
            if (id != item.ItemCode) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingItem = await _context.Items.AsNoTracking().FirstOrDefaultAsync(i => i.ItemCode == id);
                    if (existingItem == null) return NotFound();

                    item.CreatedBy = existingItem.CreatedBy;
                    item.CreatedDate = existingItem.CreatedDate;
                    item.LastModifiedBy = User.Identity?.Name ?? "System";
                    item.LastModifiedDate = DateTime.UtcNow;

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Items.Any(e => e.ItemCode == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }


        public async Task<IActionResult> DeleteItem(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
  
        public async Task<IActionResult> DeleteConfirmedItem(string id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetailsItem(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}
