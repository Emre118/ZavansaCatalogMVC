using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Data;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class MessagesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MessagesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _context.ContactMessages
            .Include(message => message.Product)
            .OrderByDescending(message => message.CreatedAt)
            .ToListAsync();

        return View(messages);
    }

    public async Task<IActionResult> Details(int id)
    {
        var message = await _context.ContactMessages
            .Include(item => item.Product)
            .FirstOrDefaultAsync(item => item.Id == id);

        return message == null ? NotFound() : View(message);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        await UpdateStatus(id, MessageStatus.Read, "Mesaj okundu olarak işaretlendi.");
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsReplied(int id)
    {
        await UpdateStatus(id, MessageStatus.Replied, "Mesaj cevaplandı olarak işaretlendi.");
        return RedirectToAction(nameof(Details), new { id });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var message = await _context.ContactMessages.FirstOrDefaultAsync(item => item.Id == id);
        return message == null ? NotFound() : View(message);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message != null)
        {
            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Mesaj silindi.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task UpdateStatus(int id, MessageStatus status, string successMessage)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message == null)
        {
            return;
        }

        message.Status = status;
        await _context.SaveChangesAsync();
        TempData["Success"] = successMessage;
    }
}
