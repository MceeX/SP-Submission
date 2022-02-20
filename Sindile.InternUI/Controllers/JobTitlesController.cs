using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sindile.InternUI.APIHelper;
using Sindile.InternUI.Models;
using Sindile.InternUI.Settings;

namespace Sindile.InternUI.Controllers
{

  public class JobTitlesController : Controller
  {
    private readonly IAPIService _apiService;
    private readonly IOptions<IntegratedAPISettings> _settings;
    private static string _resource = string.Empty;

    /// <summary>
    /// Initializes the constructor with the api service and config options
    /// </summary>
    /// <param name="apiService"></param>
    /// <param name="settings"></param>
    public JobTitlesController(IAPIService apiService, IOptions<IntegratedAPISettings> settings)
    {
      _apiService = apiService;
      _settings = settings;
      _resource = "/JobTitles";
    }

    // GET: JobTitlesController
    public async Task<ActionResult> Index()
    {
      List<JobTitle> jobTitle = new List<JobTitle>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<JobTitle>>(res);

        return View("_JobTitlesListView", result);
      }

      return View(jobTitle);
    }

    [HttpGet]
    // GET: JobTitlesController/Details/5
    public async Task<ActionResult> Details(int id)
    {
      List<JobTitle> jobTitles = new List<JobTitle>();
      var uri = new Uri($"{ _settings.Value.AdminAPIEndpoint}{_resource}/{id}");
      var response = await _apiService.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        var res = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<JobTitle>(res);
        
        return View("_JobTitleDetailsView", result);
      }
      return View();
    }

    /// <summary>
    /// Retrieves the Create View
    /// </summary>
    /// <returns></returns>
    // GET: JobTitlesController/Create
    public ActionResult Create()
    {
      return View("_CreateJobTitle");
    }

    /// <summary>
    /// Accepts requests to create a job title
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(JobTitle model)
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

    /// <summary>
    /// Retrieves the update/edit view
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: JobTitlesController/Edit/5
    public ActionResult Edit(int id)
    {
      ViewData["id"] = id;
      return View("_EditJobTitleView");
    }

    /// <summary>
    /// Accepts job title update requests 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: JobTitlesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, JobTitle model)
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

    /// <summary>
    /// Retrieves a view for job title delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: JobTitlesController/Delete/5
    public ActionResult Delete(int id)
    {
      return View("_DeleteView");
    }

    /// <summary>
    /// Accepts delete job title deletion requests
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: JobTitlesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteTitle(int id, JobTitle model)
    {
      try
      {
        string requestContentJson = JsonConvert.SerializeObject(model);

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
