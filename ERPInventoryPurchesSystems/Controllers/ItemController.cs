
using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class ItemController : Controller
{
    private readonly ApplicationDbContext _context;

    public ItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> ItemList()
    {
        var entities = await _context.Items
            .Include(i => i.Category)
            .Include(i => i.PreferredVendor)
            .ToListAsync();
        return View(entities);
    }

    [HttpGet]
    public IActionResult CreateItem()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryCode", "CategoryName");
        ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AfterCreateItem(Item entity)
    {
       

        entity.CreatedDate = DateTime.UtcNow;
        entity.LastModifiedDate = DateTime.UtcNow;
        entity.CreatedBy = "System";
        entity.LastModifiedBy = "System";

        _context.Items.Add(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsItem), new { id = entity.ItemCode });
    }

    public async Task<IActionResult> EditItem(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Items
            .Include(i => i.Category)
            .Include(i => i.PreferredVendor)
            .FirstOrDefaultAsync(i => i.ItemCode == id);
        if (entity == null) return NotFound();

        ViewBag.Categories = new SelectList(_context.Categories, "CategoryCode", "CategoryName", entity.ItemCategoryCode);
        ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", entity.PreferredVendorCode);
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> AfterEditItem(string id, Item entity)
    {
        if (id != entity.ItemCode) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryCode", "CategoryName", entity.ItemCategoryCode);
            ViewBag.Vendors = new SelectList(_context.Vendors, "VendorCode", "VendorName", entity.PreferredVendorCode);
            return View("EditItem", entity);
        }

        entity.LastModifiedDate = DateTime.UtcNow;
        entity.LastModifiedBy = User.Identity.Name;

        _context.Items.Update(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsItem), new { id = entity.ItemCode });
    }

    public async Task<IActionResult> DeleteItem(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Items
            .Include(i => i.Category)
            .Include(i => i.PreferredVendor)
            .FirstOrDefaultAsync(i => i.ItemCode == id);
        if (entity == null) return NotFound();

        return View(entity);
    }

    [HttpPost, ActionName("DeleteItem")]
    public async Task<IActionResult> DeleteItemConfirmed(string id)
    {
        var entity = await _context.Items.FirstOrDefaultAsync(i => i.ItemCode == id);
        if (entity == null) return NotFound();

        _context.Items.Remove(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ItemList));
    }

    public async Task<IActionResult> DetailsItem(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var entity = await _context.Items
            .Include(i => i.Category)
            .Include(i => i.PreferredVendor)
            .FirstOrDefaultAsync(i => i.ItemCode == id);
        if (entity == null) return NotFound();

        return View(entity);
    }
}
