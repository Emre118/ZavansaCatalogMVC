using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class CollectionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CollectionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.ProductCollections.OrderBy(collection => collection.DisplayOrder).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var collection = await _context.ProductCollections
            .Include(item => item.Products)
            .FirstOrDefaultAsync(item => item.Id == id);

        return collection == null ? NotFound() : View(collection);
    }

    public IActionResult Create()
    {
        return View(new ProductCollection { IsActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCollection collection)
    {
        if (!ModelState.IsValid)
        {
            return View(collection);
        }

        _context.ProductCollections.Add(collection);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Koleksiyon oluşturuldu.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var collection = await _context.ProductCollections.FindAsync(id);
        return collection == null ? NotFound() : View(collection);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductCollection collection)
    {
        if (id != collection.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(collection);
        }

        _context.Update(collection);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Koleksiyon güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var collection = await _context.ProductCollections.FirstOrDefaultAsync(item => item.Id == id);
        return collection == null ? NotFound() : View(collection);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var collection = await _context.ProductCollections.Include(item => item.Products).FirstOrDefaultAsync(item => item.Id == id);
        if (collection == null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (collection.Products.Any())
        {
            TempData["Error"] = "Bu koleksiyona bağlı ürünler olduğu için silinemez.";
            return RedirectToAction(nameof(Index));
        }

        _context.ProductCollections.Remove(collection);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Koleksiyon silindi.";
        return RedirectToAction(nameof(Index));
    }
}
