using System.Net.Http.Headers;

namespace Sindile.InternUI.APIHelper
{
  /// <summary>
  /// Defines the methods to expose basic HTTP operations make rest api calls
  /// </summary>
  public interface IAPIService
  {
    /// <summary>
    ///  sends a POST request as an asynchronous operation to the specified URI with a payload.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content">a http entity body and content headers.</param>
    /// <returns></returns>
    Task<HttpResponseMessage> PostAsync(Uri uri, string content);

    /// <summary>
    ///  sends a PUT request as an asynchronous operation to the specified URI with a payload.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content">a http entity body and content headers.</param>
    /// <returns></returns>
    Task<HttpResponseMessage> PutAsync(Uri uri, string content);

    /// <summary>
    ///  sends a GET request as an asynchronous operation to the specified URI.
    /// </summary>  
    /// <returns></returns>
    Task<HttpResponseMessage> GetAsync(Uri uri);

    /// <summary>
    ///  sends a GET request as an asynchronous operation to the specified URI.
    /// </summary>  
    /// <returns></returns>
    Task<HttpResponseMessage> GetAsync(Uri uri, AuthenticationHeaderValue authenticationHeaderValue);

    /// <summary>
    ///  sends a DELETE request as an asynchronous operation to the specified URI.
    /// </summary>  
    /// <returns></returns>
    Task<HttpResponseMessage> DeleteAsync(Uri uri);

    /// <summary>
    ///  sends a POST request as an asynchronous operation to the specified Uri.
    ///  sends out a FormUrlEncodedContent http reques type.
    /// </summary>
    /// <param name="authenticationHeaderValue">the authentication header value.</param>
    /// <param name="parameters">the collection of request parameters.</param>
    /// <param name="uri">resource endpoint.</param>
    /// <returns></returns>
    Task<HttpResponseMessage> PostAsync(AuthenticationHeaderValue authenticationHeaderValue, Dictionary<string, string> parameters, string uri);

    /// <summary>
    ///  sends a POST request as an asynchronous operation to the specified URI with a payload.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content">a http entity body and content headers.</param>
    /// <param name="token">Access token.</param>
    /// <returns></returns>
    Task<HttpResponseMessage> PostAsync(Uri uri, string content, string token);
  }
}
