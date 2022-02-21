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
      List<EmployeeLog> loggedTasks = new List<EmployeeLog>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/GetAllEmployeeLogs");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();
        ViewData["UsersDropList"] = GetUsersList().Result;
        var result = JsonConvert.DeserializeObject<IEnumerable<EmployeeLog>>(res);

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

        var result = JsonConvert.DeserializeObject<EmployeeLog>(res);

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
      ViewData["UsersDropList"] = GetUsersList().Result;
      ViewData["TasksDropList"] = GetTasksList().Result;
      return View("_CreateEmployeeLog");
    }

    /// <summary>
    /// Accepts requests to log time against a task
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: TaskLogsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(EmployeeLog model)
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
    public async Task<ActionResult> Edit(int id, EmployeeLog model)
    {
      try
      {
        var requestModel = new TaskLog
        {
          Id = id,
          TaskId = model.TaskId,
          UserId = model.UserId,
          HourlyRate = model.HourlyRate,
          Duration = model.Duration
        };

        string requestContentJson = JsonConvert.SerializeObject(requestModel);
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


    /// <summary>
    /// Retrieves a list of users
    /// </summary>
    /// <returns></returns>
    // GET: TaskLogsController
    [NonAction]
    public async Task<IEnumerable<UserDropdown>> GetUsersList()
    {
      List<UserDropdown> users = new List<UserDropdown>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Users");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<IEnumerable<User>>(res);

        var usersList = from r in result
               select new UserDropdown
               {
                 Id = r.Id,
                 FirstName = r.FirstName
               };
        return usersList;
      }
      return users;
    }

    /// <summary>
    /// Retrieves a list of tasks
    /// </summary>
    /// <returns></returns>
    // GET: TaskLogsController
    [NonAction]
    public async Task<IEnumerable<WorkTask>> GetTasksList()
    {
      List<WorkTask> tasks = new List<WorkTask>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}/Tasks");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<IEnumerable<WorkTask>>(res);

        return result;
      }
      return tasks;
    }
  }
}
