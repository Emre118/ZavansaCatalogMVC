using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ProductsController : Controller
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxUploadSize = 5 * 1024 * 1024;
    private const string PlaceholderImage = "/images/placeholders/product-placeholder.svg";

    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(product => product.Category)
            .Include(product => product.ProductCollection)
            .OrderBy(product => product.Name)
            .ToListAsync();

        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(item => item.Category)
            .Include(item => item.ProductCollection)
            .FirstOrDefaultAsync(item => item.Id == id);

        return product == null ? NotFound() : View(product);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateDropdowns();
        return View(new Product { IsActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
    {
        if (!await ValidateUpload(imageFile))
        {
            await PopulateDropdowns(product.CategoryId, product.ProductCollectionId);
            return View(product);
        }

        if (!ModelState.IsValid)
        {
            await PopulateDropdowns(product.CategoryId, product.ProductCollectionId);
            return View(product);
        }

        product.ImageUrl = await SaveImage(imageFile) ?? PlaceholderImage;
        product.CreatedAt = DateTime.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Ürün başarıyla oluşturuldu.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await PopulateDropdowns(product.CategoryId, product.ProductCollectionId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        if (!await ValidateUpload(imageFile))
        {
            product.ImageUrl = existingProduct.ImageUrl;
            await PopulateDropdowns(product.CategoryId, product.ProductCollectionId);
            return View(product);
        }

        if (!ModelState.IsValid)
        {
            product.ImageUrl = existingProduct.ImageUrl;
            await PopulateDropdowns(product.CategoryId, product.ProductCollectionId);
            return View(product);
        }

        product.CreatedAt = existingProduct.CreatedAt;
        product.UpdatedAt = DateTime.UtcNow;
        product.ImageUrl = await SaveImage(imageFile) ?? existingProduct.ImageUrl ?? PlaceholderImage;

        _context.Update(product);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Ürün başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products
            .Include(item => item.Category)
            .Include(item => item.ProductCollection)
            .FirstOrDefaultAsync(item => item.Id == id);

        return product == null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Ürün silindi.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDropdowns(int? categoryId = null, int? collectionId = null)
    {
        ViewBag.CategoryId = new SelectList(await _context.Categories.OrderBy(item => item.Name).ToListAsync(), "Id", "Name", categoryId);
        ViewBag.ProductCollectionId = new SelectList(await _context.ProductCollections.OrderBy(item => item.DisplayOrder).ToListAsync(), "Id", "Name", collectionId);
    }

    private async Task<string?> SaveImage(IFormFile? imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null;
        }

        var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "products");
        Directory.CreateDirectory(uploadsPath);
        var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{extension}";
        var physicalPath = Path.Combine(uploadsPath, fileName);

        await using var stream = new FileStream(physicalPath, FileMode.Create);
        await imageFile.CopyToAsync(stream);

        return $"/uploads/products/{fileName}";
    }

    private Task<bool> ValidateUpload(IFormFile? imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return Task.FromResult(true);
        }

        var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
        {
            ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png ve .webp dosyaları yüklenebilir.");
            return Task.FromResult(false);
        }

        if (imageFile.Length > MaxUploadSize)
        {
            ModelState.AddModelError("ImageUrl", "Görsel dosyası en fazla 5 MB olabilir.");
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}
