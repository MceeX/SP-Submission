using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sindile.InternUI.APIHelper;
using Sindile.InternUI.Models;
using Sindile.InternUI.Settings;

namespace Sindile.InternUI.Controllers
{
  public class UsersController : Controller
  {
    private readonly IAPIService _apiService;
    private readonly IOptions<IntegratedAPISettings> _settings;
    private static string _resource = string.Empty;
    private static string _errorHandlingView = string.Empty;

    /// <summary>
    /// Initializes the constructor with the api service and config options
    /// </summary>
    /// <param name="apiService"></param>
    /// <param name="settings"></param>
    public UsersController(IAPIService apiService, IOptions<IntegratedAPISettings> settings)
    {
      _apiService = apiService;
      _settings = settings;
      _resource = "/Users";
      _errorHandlingView = "~/Views/Shared/Error";
    }

    /// <summary>
    /// Retrieves a list of employees
    /// </summary>
    /// <returns></returns>
    // GET: UsersController
    public async Task<ActionResult> Index()
    {
      List<User> users = new List<User>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<User>>(res);
        return View("_UserListView", result);
      }

      return View(_errorHandlingView);
    }

    /// <summary>
    /// Retrieves a specific employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: UsersController/Details/5
    public async Task<ActionResult> Details(int id)
    {
      User user = new User();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/{id}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<User>(res);
        return View("_UserDetailsView", result);
      }
      return View(_errorHandlingView);
    }

    /// <summary>
    /// Retrieves view to create an empoyee
    /// </summary>
    /// <returns></returns>
    //// GET: UsersController/Create
    public ActionResult Create()
    {
      ViewData["TitlesDropList"] = GetTitleList().Result;
      return View("Create");
    }

    /// <summary>
    /// Accepts requests to create a new employee
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: UsersController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(User model)
    {      
      try
      {
        string requestContentJson = JsonConvert.SerializeObject(model);

        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}");
        var response = await _apiService.PostAsync(uri, requestContentJson);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
      return View(_errorHandlingView);
    }

    /// <summary>
    /// Retrieves job title list
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public async Task<IEnumerable<JobTitle>> GetTitleList()
    {
      List<JobTitle> roles = new List<JobTitle>();

      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/JobTitles");
      var response = await _apiService.GetAsync(uri);

      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<JobTitle>>(res);

        return result;
      }
      return roles;
    }

    /// <summary>
    /// Retrieves view to update employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: UsersController/Edit/5
    public ActionResult Edit(int id)
    {
      return View("_EditUserView");
    }

    /// <summary>
    /// Accepts requests to update empoyee
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: UsersController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, User model)
    {
      try
      {
        string requestContentJson = JsonConvert.SerializeObject(model);

        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/{id}");
        var response = await _apiService.PutAsync(uri, requestContentJson);
        return RedirectToAction(nameof(Details), new { id = id });
      }
      catch
      {
        return View(_errorHandlingView);
      }
    }

    /// <summary>
    /// Retrieves the view to dismiss an employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: UsersController/Delete/5
    public ActionResult Dismiss(int id)
    {
      return View("_DismissEmployeeView");
    }

    /// <summary>
    /// Accepts requests to dismiss an employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // POST: UsersController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DismissEmployee(int id)
    {
      try
      {
        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/Dismiss/{id}");
        var response = await _apiService.DeleteAsync(uri/*, requestContentJson*/);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View(_errorHandlingView);
      }
    }
  }
}
