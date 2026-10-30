using CommunityNoticeBoard.Data;
using CommunityNoticeBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommunityNoticeBoard.Controllers;

[Authorize]
public class NoticesController : Controller
{
    private readonly ApplicationDbContext _context;
    public NoticesController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? search, string? category)
    {
        var query = _context.Notices.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(n => n.Title.Contains(search) || n.Description.Contains(search));
        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(n => n.Category == category);
        ViewBag.Search = search;
        ViewBag.Category = category;
        ViewBag.Categories = await _context.Notices.Select(n => n.Category).Distinct().OrderBy(c => c).ToListAsync();
        return View(await query.OrderByDescending(n => n.IsImportant).ThenByDescending(n => n.PublishedOn).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var notice = await _context.Notices.FirstOrDefaultAsync(n => n.Id == id);
        return notice == null ? NotFound() : View(notice);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View(new Notice { PublishedOn = DateTime.Today });

    [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Notice notice)
    {
        if (!ModelState.IsValid) return View(notice);
        _context.Add(notice);
        await _context.SaveChangesAsync();
        TempData["Message"] = "Notice published successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var notice = await _context.Notices.FindAsync(id);
        return notice == null ? NotFound() : View(notice);
    }

    [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, Notice notice)
    {
        if (id != notice.Id) return NotFound();
        if (!ModelState.IsValid) return View(notice);
        _context.Update(notice);
        await _context.SaveChangesAsync();
        TempData["Message"] = "Notice updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var notice = await _context.Notices.FirstOrDefaultAsync(n => n.Id == id);
        return notice == null ? NotFound() : View(notice);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var notice = await _context.Notices.FindAsync(id);
        if (notice != null) _context.Notices.Remove(notice);
        await _context.SaveChangesAsync();
        TempData["Message"] = "Notice deleted.";
        return RedirectToAction(nameof(Index));
    }
}
