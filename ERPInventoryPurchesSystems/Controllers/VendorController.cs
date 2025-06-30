using ERPInventoryPurchesSystems.Models.Master;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class VendorController : Controller
{
    private readonly ApplicationDbContext _context;

    public VendorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List all vendors
    public async Task<IActionResult> VendorList()
    {
        var vendors = await _context.Vendors.Include(v => v.Category).ToListAsync();
        return View(vendors);
    }

    // GET: Create Vendor
    public IActionResult CreateVendor()
    {
        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryCode", "CategoryName");
        return View();
    }

    // POST: Create Vendor
    [HttpPost]
    public async Task<IActionResult> AfterCreateVendor(Vendor vendor)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryCode", "CategoryName");
            return View("CreateVendor", vendor);
        }

        vendor.CreatedDate = DateTime.UtcNow;
        vendor.LastModifiedDate = DateTime.UtcNow;

        _context.Add(vendor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(VendorList));
    }

    // GET: Edit Vendor
    public async Task<IActionResult> EditVendor(string id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryCode", "CategoryName", vendor.CategoryCode);
        return View(vendor);
    }

    // POST: Edit Vendor
    [HttpPost]
    public async Task<IActionResult> AfterEditVendor(string id, Vendor vendor)
    {
        if (id != vendor.VendorCode) return BadRequest("Invalid vendor code.");

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryCode", "CategoryName", vendor.CategoryCode);
            return View("EditVendor", vendor);
        }

        var existingVendor = await _context.Vendors.AsNoTracking().FirstOrDefaultAsync(v => v.VendorCode == id);
        if (existingVendor == null) return NotFound();

        vendor.CreatedDate = existingVendor.CreatedDate;
        vendor.LastModifiedDate = DateTime.UtcNow;

        _context.Update(vendor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(VendorList));
    }

    // GET: Delete Vendor
    public async Task<IActionResult> DeleteVendor(string id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();
        return View(vendor);
    }

    // POST: Confirm Delete
    [HttpPost, ActionName("DeleteVendor")]
    public async Task<IActionResult> DeleteVendorConfirmed(string id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();

        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(VendorList));
    }

    // GET: Vendor Details
    public async Task<IActionResult> DetailsVendor(string id)
    {
        var vendor = await _context.Vendors.Include(v => v.Category).FirstOrDefaultAsync(v => v.VendorCode == id);
        if (vendor == null) return NotFound();
        return View(vendor);
    }
}
