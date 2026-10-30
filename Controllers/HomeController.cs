using CommunityNoticeBoard.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommunityNoticeBoard.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    public HomeController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var notices = await _context.Notices
            .OrderByDescending(n => n.IsImportant)
            .ThenByDescending(n => n.PublishedOn)
            .Take(6)
            .ToListAsync();
        return View(notices);
    }

    public IActionResult Privacy() => View();
}
