using Microsoft.AspNetCore.Mvc;
using Sindile.InternUI.Models;
using System.Diagnostics;

namespace Sindile.InternUI.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
  
      return View();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItern(Intern model)
    {
        //_logger.LogInformation("User forced to reset password, since its their first login");


        return View("_InternListView");
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}