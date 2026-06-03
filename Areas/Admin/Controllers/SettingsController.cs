using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class SettingsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SettingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var settings = await GetOrCreateSettings();
        return View(settings);
    }

    public async Task<IActionResult> Edit()
    {
        var settings = await GetOrCreateSettings();
        return View(settings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SiteSetting settings)
    {
        if (id != settings.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(settings);
        }

        _context.Update(settings);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Site ayarları güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<SiteSetting> GetOrCreateSettings()
    {
        var settings = await _context.SiteSettings.FirstOrDefaultAsync();
        if (settings != null)
        {
            return settings;
        }

        settings = new SiteSetting
        {
            BrandName = "Zavansa Aydınlatma ve Dekor",
            Slogan = "Modern yaşam alanları için özgün aydınlatma tasarımları",
            AboutText = "Zavansa, modern yaşam alanları için dekoratif aydınlatma ürünleri sunan katalog odaklı bir markadır.",
            CatalogPdfUrl = "/catalog/zavansa-katalog.pdf"
        };

        _context.SiteSettings.Add(settings);
        await _context.SaveChangesAsync();
        return settings;
    }
}
