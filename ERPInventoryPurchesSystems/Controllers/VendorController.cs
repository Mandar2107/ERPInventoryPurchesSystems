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

    public async Task<IActionResult> VendorList()
    {
        var vendors = await _context.Vendors.Include(v => v.Category).ToListAsync();
        return View(vendors);
    }

    [HttpGet]
    public IActionResult CreateVendor()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryCode", "CategoryName");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> AfterCreateVendor([FromForm]  Vendor vendor)
    {
        vendor.CreatedDate = DateTime.UtcNow;
        vendor.LastModifiedDate = DateTime.UtcNow;
        vendor.CreatedBy = vendor.VendorName ?? "System";
        vendor.LastModifiedBy = vendor.VendorName ?? "System";


        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsVendor), new { id = vendor.VendorCode });
    }


    public async Task<IActionResult> EditVendor(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return NotFound();

        ViewBag.Categories = new SelectList(_context.Categories, "CategoryCode", "CategoryName", vendor.CategoryCode);
        return View(vendor);
    }

    [HttpPost]
    public async Task<IActionResult> AfterEditVendor(string id, Vendor vendor)
    {
        if (id != vendor.VendorCode) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryCode", "CategoryName", vendor.CategoryCode);
            return View("EditVendor", vendor);
        }

        vendor.LastModifiedDate = DateTime.UtcNow;
        vendor.LastModifiedBy = User.Identity.Name;

        _context.Vendors.Update(vendor);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(DetailsVendor), new { id = vendor.VendorCode });
    }

    public async Task<IActionResult> DeleteVendor(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

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
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var vendor = await _context.Vendors
            .Include(v => v.Category)
            .FirstOrDefaultAsync(v => v.VendorCode == id);

        if (vendor == null) return NotFound();

        return View(vendor);
    }
}
