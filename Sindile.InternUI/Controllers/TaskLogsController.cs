using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sindile.InternUI.APIHelper;
using Sindile.InternUI.Models;
using Sindile.InternUI.Settings;

namespace Sindile.InternUI.Controllers
{
  public class TaskLogsController : Controller
  {
    private readonly IAPIService _apiService;
    private readonly IOptions<IntegratedAPISettings> _settings;
    private static string _resource = string.Empty;

    /// <summary>
    /// Initializes the constructor with the api service and config options
    /// </summary>
    /// <param name="apiService"></param>
    /// <param name="settings"></param>
    public TaskLogsController(IAPIService apiService, IOptions<IntegratedAPISettings> settings)
    {
      _apiService = apiService;
      _settings = settings;
      _resource = "/TaskLogs";
    }

    /// <summary>
    /// Presents a list of task logs
    /// </summary>
    /// <returns></returns>
    // GET: TaskLogsController
    public async Task<ActionResult> Index()
    {
      List<TaskLog> loggedTasks = new List<TaskLog>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/GetAllLogsByEmployee");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<TaskLog>>(res);

        return View("_TaskLogsListView", result);
      }
        return View(loggedTasks);
    }

    /// <summary>
    /// Presents a specific task log
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: TaskLogsController/Details/5
    public async Task<ActionResult> Details(int id)
    {
      List<EmployeeLog> loggedTasks = new List<EmployeeLog>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/GetLogsByEmployee/{id}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<TaskLog>(res);

        return View("_TaskLogDetailsView", result);
      }
      return View(loggedTasks);
    }

    /// <summary>
    /// Retrieves the Create View
    /// </summary>
    /// <returns></returns>
    // GET: TaskLogsController/Create
    public ActionResult Create()
    {
      return View("_CreateTaskLogView");
    }

    /// <summary>
    /// Accepts requests to log time against a task
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: TaskLogsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(TaskLog model)
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
    }

    // GET: TaskLogsController/Edit/5
    public ActionResult Edit(int id)
    {
      return View("_EditTaskLogView");
    }

    /// <summary>
    /// Accepts task log update requests
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: TaskLogsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, TaskLog model)
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
        return View();
      }
    }

    // GET: TaskLogsController/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: TaskLogsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteTaskLog(int id)
    {
      try
      {
        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/{id}");
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
