using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Settings = await _context.SiteSettings.FirstOrDefaultAsync();
        ViewBag.Collections = await _context.ProductCollections
            .Where(collection => collection.IsActive)
            .OrderBy(collection => collection.DisplayOrder)
            .Take(4)
            .ToListAsync();

        var products = await _context.Products
            .Include(product => product.Category)
            .Include(product => product.ProductCollection)
            .Where(product => product.IsActive && product.IsFeatured)
            .OrderBy(product => product.Name)
            .Take(6)
            .ToListAsync();

        return View(products);
    }

    public async Task<IActionResult> About()
    {
        var settings = await _context.SiteSettings.FirstOrDefaultAsync();
        return View(settings);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
