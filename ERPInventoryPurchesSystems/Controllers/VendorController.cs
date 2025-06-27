using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;

public class VendorController : Controller
{
    private readonly ApplicationDbContext _context;

    public VendorController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> VendorList()
    {
        return View(await _context.Vendors.ToListAsync());
    }

    public IActionResult CreateVendor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateVendor(Vendor vendor)
    {
        if (!ModelState.IsValid) return View(vendor);

        vendor.CreatedDate = DateTime.UtcNow;
        vendor.LastModifiedDate = DateTime.UtcNow;

        _context.Add(vendor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(VendorList));
    }

    public async Task<IActionResult> EditVendor(string id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();
        return View(vendor);
    }

    [HttpPost]
    public async Task<IActionResult> EditVendor(string id, Vendor vendor)
    {
        if (id != vendor.VendorCode) return BadRequest();

        if (!ModelState.IsValid) return View(vendor);

        vendor.LastModifiedDate = DateTime.UtcNow;

        _context.Update(vendor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(VendorList));
    }

    public async Task<IActionResult> DeleteVendor(string id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();
        return View(vendor);
    }

    [HttpPost, ActionName("DeleteVendor")]
    public async Task<IActionResult> DeleteVendorConfirmed(string id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();

        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(VendorList));
    }

    public async Task<IActionResult> DetailsVendor(string id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();
        return View(vendor);
    }
}
