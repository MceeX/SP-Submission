using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sindile.InternUI.APIHelper;
using Sindile.InternUI.Models;
using Sindile.InternUI.Settings;
using System.Text.Json;

namespace Sindile.InternUI.Controllers
{
  public class UsersController : Controller
  {
    private readonly IAPIService _apiService;
    private readonly IOptions<IntegratedAPISettings> _settings;

    /// <summary>
    /// Initializes the constructor with the api service and config options
    /// </summary>
    /// <param name="apiService"></param>
    /// <param name="settings"></param>
    public UsersController(IAPIService apiService, IOptions<IntegratedAPISettings> settings)
    {
      _apiService = apiService;
      _settings = settings;
    }

    // GET: UsersController
    public async Task<IEnumerable<User>> Index()
    {
      List<User> users = new List<User>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<User>>(res);

        return result;
      }

      return users;
    }

    // GET: UsersController/Details/5
    public ActionResult Details(int id)
    {

      return View();
    }

    //// GET: UsersController/Create
    //public ActionResult Create(User model)
    //{
    //  return View();
    //}

    // POST: UsersController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    //public ActionResult Create(IFormCollection collection)
    public async Task<ActionResult> Create(User model)
    {

      //PostAsync(Uri uri, string content)
      
      string requestContentJson = JsonSerializer.Serialize(model);

      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users");
      var response = await _apiService.PostAsync(uri, requestContentJson);
      //return RedirectToAction(nameof(Index));
      //try
      //{
      //  return RedirectToAction(nameof(Index));
      //}
      //catch
      //{
      //  return View();
      //}

      return View();
    }

    [NonAction]
    public async Task<IEnumerable<Role>> GetTitleList()
    {
      List<Role> roles = new List<Role>();

      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Roles");
      var response = await _apiService.GetAsync(uri);

      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<Role>>(res);

        return result;
      }
      return roles;
    }

    // GET: UsersController/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: UsersController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: UsersController/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: UsersController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}
