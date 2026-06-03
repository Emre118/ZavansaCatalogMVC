using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.Models;
using ZavansaCatalogMVC.ViewModels;

namespace ZavansaCatalogMVC.Controllers;

public class ContactController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? productId)
    {
        var model = new ContactFormViewModel();

        if (productId.HasValue)
        {
            var product = await _context.Products.FirstOrDefaultAsync(item => item.Id == productId.Value && item.IsActive);
            if (product != null)
            {
                model.ProductId = product.Id;
                model.InterestedProductName = product.Name;
                model.Subject = $"{product.Name} hakkında bilgi talebi";
            }
        }

        ViewBag.Settings = await _context.SiteSettings.FirstOrDefaultAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Settings = await _context.SiteSettings.FirstOrDefaultAsync();
            return View(model);
        }

        var message = new ContactMessage
        {
            FullName = model.FullName,
            Email = model.Email,
            Phone = model.Phone,
            Subject = model.Subject,
            Message = model.Message,
            ProductId = model.ProductId,
            InterestedProductName = model.InterestedProductName,
            Status = MessageStatus.New,
            CreatedAt = DateTime.UtcNow
        };

        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Mesajınız başarıyla kaydedildi. En kısa sürede sizinle iletişime geçilecektir.";
        return RedirectToAction(nameof(Index));
    }
}
