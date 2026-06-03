using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;

namespace ZavansaCatalogMVC.Controllers;

public class CatalogController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public CatalogController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var settings = await _context.SiteSettings.FirstOrDefaultAsync();
        var catalogUrl = settings?.CatalogPdfUrl ?? "/catalog/zavansa-katalog.pdf";
        var relativePath = catalogUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        var physicalPath = Path.Combine(_environment.WebRootPath, relativePath);

        ViewBag.CatalogUrl = catalogUrl;
        ViewBag.CatalogExists = System.IO.File.Exists(physicalPath);

        return View();
    }
}
