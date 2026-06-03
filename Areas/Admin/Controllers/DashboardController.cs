using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.Models;
using ZavansaCatalogMVC.ViewModels;

namespace ZavansaCatalogMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            TotalProductCount = await _context.Products.CountAsync(),
            ActiveProductCount = await _context.Products.CountAsync(product => product.IsActive),
            CollectionCount = await _context.ProductCollections.CountAsync(),
            UnreadMessageCount = await _context.ContactMessages.CountAsync(message => message.Status == MessageStatus.New),
            LatestMessages = await _context.ContactMessages
                .OrderByDescending(message => message.CreatedAt)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }
}
