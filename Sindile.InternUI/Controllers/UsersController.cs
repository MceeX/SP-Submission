using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
    public async Task<ActionResult> Index()
    {
      List<User> users = new List<User>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<User>>(res);
        //ViewData["model"] = result.ToList();
        return View("_UserListView", result);
      }

      return View(users);
    }

    // GET: UsersController/Details/5
    public async Task<ActionResult> Details(int id)
    {
      User user = new User();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users/{id}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<User>(res);
        //ViewData["model"] = result.ToList();
        return View("_UserDetailsView", result);
      }
      return View(user);
    }

    //// GET: UsersController/Create
    public async Task<ActionResult> Create()
    {
      return View("Create");//"~/Views/Users/Create.cshtml")
    }

    // POST: UsersController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(User model)
    {      
      try
      {
        string requestContentJson = JsonConvert.SerializeObject(model);

        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users");
        var response = await _apiService.PostAsync(uri, requestContentJson);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
      return View();
    }

    [NonAction]
    public async Task<IEnumerable<JobTitle>> GetTitleList()
    {
      List<JobTitle> roles = new List<JobTitle>();

      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Roles");
      var response = await _apiService.GetAsync(uri);

      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<JobTitle>>(res);

        return result;
      }
      return roles;
    }

    // GET: UsersController/Edit/5
    public ActionResult Edit(int id)
    {
      return View("_EditUserView");
    }

    // POST: UsersController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, User model)
    {
      try
      {
        string requestContentJson = JsonConvert.SerializeObject(model);

        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users/{id}");
        var response = await _apiService.PutAsync(uri, requestContentJson);
        return RedirectToAction(nameof(Index),id);
      }
      catch
      {
        return View();
      }
    }

    // GET: UsersController/Delete/5
    public ActionResult Delete(int id)
    {
      //return View();
      return RedirectToAction(nameof(DeleteUser), id);
    }

    // POST: UsersController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteUser(int id)
    {
      try
      {
        //string requestContentJson = JsonConvert.SerializeObject(model);
        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users/{id}");
        var response = await _apiService.DeleteAsync(uri/*, requestContentJson*/);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}
