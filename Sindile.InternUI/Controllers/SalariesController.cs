using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sindile.InternUI.APIHelper;
using Sindile.InternUI.Models;
using Sindile.InternUI.Settings;

namespace Sindile.InternUI.Controllers
{
  public class SalariesController : Controller
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
    public SalariesController(IAPIService apiService, IOptions<IntegratedAPISettings> settings)
    {
      _apiService = apiService;
      _settings = settings;
      _resource = "/Users";
      _errorHandlingView = "~/Views/Shared/Error";
    }

    /// <summary>
    /// Retrieves a list of employee salaries
    /// </summary>
    /// <returns></returns>
    // GET: SalariesController
    public ActionResult Index()
    {
      return View("_SalariesCalcView");
    }

    /// <summary>
    /// Retrieves all employees salaries
    /// </summary>
    /// <param name="id"></param>
    /// <param name="startDateTime"></param>
    /// <param name="endDateTime"></param>
    /// <returns></returns>
    // GET: SalariesController/Details/5
    public async Task<ActionResult> GetAllEmployeeSalaries(DateRange model/*DateTime startDateTime, DateTime endDateTime*/)
    {
      List<EmployeeSalary> salaries = new List<EmployeeSalary>();

      string requestContentJson = JsonConvert.SerializeObject(model);

      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/CalculateSalary");
      var response = await _apiService.PostAsync(uri, requestContentJson);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<EmployeeSalary>>(res);
        //ViewData["model"] = result.ToList();
        return View("_EmployeeSalaryListView", result);
      }
      return View();
    }

    /// <summary>
    /// Retrieves the details request view
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: SalariesController/Details/
    [Route("Details/{Id}")]
    public async Task<ActionResult> Details(int id)
    {
      ViewData["Id"] = id;
      return View("_CalcSalryForEmployee");
    }

    /// <summary>
    /// Calculates a single employees salary
    /// </summary>
    /// <param name="id"></param>
    /// <param name="startDateTime"></param>
    /// <param name="endDateTime"></param>
    /// <returns></returns>
    // GET: SalariesController/Details/5
    public async Task<ActionResult> Details(int id, DateTime startDateTime, DateTime endDateTime)
    {
      List<EmployeeSalary> salaries = new List<EmployeeSalary>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/CalculateSalary/{id}/{startDateTime}/{endDateTime}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<EmployeeSalary>>(res);
        //ViewData["model"] = result.ToList();
        return View("_UserListView", result);
      }
      return View(_errorHandlingView);
    }

    // GET: SalariesController/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: SalariesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
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

    // GET: SalariesController/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: SalariesController/Edit/5
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

    // GET: SalariesController/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: SalariesController/Delete/5
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
