using System.Net.Http.Headers;
using System.Text;

namespace Sindile.InternUI.APIHelper
{
  /// <summary>
  /// Provides operations to make basic HTTP calls
  /// </summary>
  public class APIService : IAPIService
  {
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initialises the constructor
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public APIService(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }
    /// <summary>
    /// Makes a HTTP GET to an external API
    /// </summary>
    /// <returns></returns>
    public async Task<HttpResponseMessage> GetAsync(Uri uri)
    {
      var httpClient = _httpClientFactory.CreateClient();

      string url = uri.ToString();
      httpClient.BaseAddress = new Uri(uri.ToString());

      var response = await httpClient.GetAsync(url).ConfigureAwait(false);
      return await Task.FromResult(response);

    }

    /// <summary>
    /// Makes a HTTP GET to an external API
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="authenticationHeaderValue"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> GetAsync(Uri uri, AuthenticationHeaderValue authenticationHeaderValue)
    {
      var httpClient = _httpClientFactory.CreateClient();

      string url = uri.ToString();
      httpClient.BaseAddress = new Uri(uri.ToString());
      httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

      var response = await httpClient.GetAsync(url).ConfigureAwait(false);
      return await Task.FromResult(response);
    }

    /// <summary>
    /// Makes a HTTP POST to an external API
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> PostAsync(Uri uri, string content)
    {
      var httpClient = _httpClientFactory.CreateClient();
      string url = uri.ToString();

      httpClient.BaseAddress = new Uri(uri.ToString());

      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var response = await httpClient.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json")).ConfigureAwait(false);
      return await Task.FromResult(response);

    }

    /// <summary>
    /// Makes a HTTP PUT to an external API
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> PutAsync(Uri uri, string content)
    {
      var httpClient = _httpClientFactory.CreateClient();

      string url = uri.ToString();
      httpClient.BaseAddress = new Uri(uri.ToString());

      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var response = await httpClient.PutAsync(url, new StringContent(content, Encoding.UTF8, "application/json")).ConfigureAwait(false);
      return await Task.FromResult(response);

    }


    /// <summary>
    /// Makes a HTTP DELETE to an external API
    /// </summary>
    /// <param name="uri"></param>   
    /// <returns></returns>
    public async Task<HttpResponseMessage> DeleteAsync(Uri uri)
    {
      var httpClient = _httpClientFactory.CreateClient();
      string url = uri.ToString();

      var response = await httpClient.DeleteAsync(url).ConfigureAwait(false);
      return await Task.FromResult(response);

    }

    /// <summary>
    /// Makes a HTTP POST to an external API
    /// </summary>
    /// <param name="authenticationHeaderValue"></param>
    /// <param name="parameters"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> PostAsync(AuthenticationHeaderValue authenticationHeaderValue, Dictionary<string, string> parameters, string uri)
    {
      var httpClient = _httpClientFactory.CreateClient();

      var request = new HttpRequestMessage(HttpMethod.Post, uri);
      request.Headers.Authorization = authenticationHeaderValue;
      request.Content = new FormUrlEncodedContent(parameters);

      var response = await httpClient.SendAsync(request).ConfigureAwait(false);

      return response;

    }
    /// <summary>
    /// Makes a HTTP POST to an external API with an access token
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> PostAsync(Uri uri, string content, string token)
    {
      var httpClient = _httpClientFactory.CreateClient();

      string url = uri.ToString();
      httpClient.BaseAddress = new Uri(uri.ToString());

      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      var authValue = new AuthenticationHeaderValue("Bearer", token);
      httpClient.DefaultRequestHeaders.Authorization = authValue;

      var response = await httpClient.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json")).ConfigureAwait(false);
      return await Task.FromResult(response);

    }
  }
}
