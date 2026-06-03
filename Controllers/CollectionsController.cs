using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;

namespace ZavansaCatalogMVC.Controllers;

public class CollectionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CollectionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var collections = await _context.ProductCollections
            .Where(collection => collection.IsActive)
            .OrderBy(collection => collection.DisplayOrder)
            .ToListAsync();

        return View(collections);
    }

    public async Task<IActionResult> Details(int id)
    {
        var collection = await _context.ProductCollections
            .Include(item => item.Products.Where(product => product.IsActive))
            .ThenInclude(product => product.Category)
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        if (collection == null)
        {
            return NotFound();
        }

        return View(collection);
    }
}
