using ERPInventoryPurchesSystems.Models.PR;
using ERPInventoryPurchesSystems.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPInventoryPurchesSystems.Controllers.PRcontrollers
{
    public class PurchaseRequisitionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRequisitionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchaseRequisitions.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchesRequstiaon model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
