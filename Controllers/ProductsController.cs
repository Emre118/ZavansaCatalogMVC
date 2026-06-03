using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.ViewModels;

namespace ZavansaCatalogMVC.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? search, int? categoryId, int? collectionId)
    {
        var query = _context.Products
            .Include(product => product.Category)
            .Include(product => product.ProductCollection)
            .Where(product => product.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(product => product.Name.Contains(search));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(product => product.CategoryId == categoryId.Value);
        }

        if (collectionId.HasValue)
        {
            query = query.Where(product => product.ProductCollectionId == collectionId.Value);
        }

        var viewModel = new ProductListViewModel
        {
            Products = await query.OrderBy(product => product.Name).ToListAsync(),
            Categories = await _context.Categories.Where(category => category.IsActive).OrderBy(category => category.Name).ToListAsync(),
            Collections = await _context.ProductCollections.Where(collection => collection.IsActive).OrderBy(collection => collection.DisplayOrder).ToListAsync(),
            Search = search,
            CategoryId = categoryId,
            CollectionId = collectionId
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(item => item.Category)
            .Include(item => item.ProductCollection)
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}
