using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UI.Models;
using UI.MySession;

namespace UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var session = HttpContext.GetSession();
        session.SetString("Name", "Thanh");
        await session.CommitAsync();

        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var session = HttpContext.GetSession();
        await session.LoadAsync();
        var name = session.GetString("Name");
        return View("Privacy", name);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
