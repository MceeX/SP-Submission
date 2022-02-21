using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sindile.InternUI.APIHelper;
using Sindile.InternUI.Models;
using Sindile.InternUI.Settings;

namespace Sindile.InternUI.Controllers
{
  public class TasksController : Controller
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
    public TasksController(IAPIService apiService, IOptions<IntegratedAPISettings> settings)
    {
      _apiService = apiService;
      _settings = settings;
      _resource = "/Tasks";
      _errorHandlingView = "~/Views/Shared/Error";
    }

    /// <summary>
    /// Presents a list of tasks
    /// </summary>
    /// <returns></returns>
    // GET: TasksController
    public async Task<ActionResult> Index()
    {
      List<WorkTask> loggedTasks = new List<WorkTask>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<WorkTask>>(res);

        return View("_TaskListView", result);
      }
      return View();
    }

    /// <summary>
    /// Presents a specific task
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: TasksController/Details/5
    public async Task<ActionResult> Details(int id)
    {
      List<WorkTask> tasks = new List<WorkTask>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/{id}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<WorkTask>(res);

        return View("_TaskDetailsView", result);
      }
      return View();
    }

    /// <summary>
    /// Retrieves the create view
    /// </summary>
    /// <returns></returns>
    // GET: TasksController/Create
    public ActionResult Create()
    {
      return View("_CreateTaskView");
    }

    /// <summary>
    /// Accepts create task requests
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: TasksController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(WorkTask model)
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
        return View(_errorHandlingView);
      }
    }

    /// <summary>
    /// Retrieves the update view
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: TasksController/Edit/5
    public ActionResult Edit(int id)
    {
      return View("_EditTaskView");
    }

    /// <summary>
    /// Accepts task update requests
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: TasksController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, WorkTask model)
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
    /// Retrieves the delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: TasksController/Delete/5
    public ActionResult Delete(int id)
    {
      return View("_DeleteView");
    }

    // POST: TasksController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteTask(int id)
    {
      try
      {
        var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/{id}");
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
