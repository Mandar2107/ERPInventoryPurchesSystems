using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ItemController : Controller
{
    private readonly ApplicationDbContext _context;

    public ItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> ItemList()
    {
        return View(await _context.Items.ToListAsync());
    }

    public IActionResult CreateItem()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateItemList(Item item)
    {
        if (!ModelState.IsValid) return View(item);

        item.CreatedBy = User.Identity?.Name ?? "System";
        item.CreatedDate = DateTime.UtcNow;
        item.LastModifiedBy = item.CreatedBy;
        item.LastModifiedDate = item.CreatedDate;

        _context.Add(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ItemList));
    }

    public async Task<IActionResult> EditItem(string id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> EditItemList(string id, Item item)
    {
        if (id != item.ItemCode) return BadRequest();

        if (!ModelState.IsValid) return View(item);

        var existingItem = await _context.Items.AsNoTracking().FirstOrDefaultAsync(i => i.ItemCode == id);
        if (existingItem == null) return NotFound();

        item.CreatedBy = existingItem.CreatedBy;
        item.CreatedDate = existingItem.CreatedDate;
        item.LastModifiedBy = User.Identity?.Name ?? "System";
        item.LastModifiedDate = DateTime.UtcNow;

        _context.Update(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ItemList));
    }

    public async Task<IActionResult> DeleteItem(string id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("DeleteItem")]
    public async Task<IActionResult> DeleteItemConfirmed(string id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) return NotFound();

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ItemList));
    }

    public async Task<IActionResult> DetailsItem(string id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }
}
