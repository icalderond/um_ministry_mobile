using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using UMMinistry.Core.Constants;
using UMMinistry.Core.Interfaces.Services;
using UMMinistry.Core.Models.General;
using UMMinistry.Core.Models.HttpHelpers;

namespace UMMinistry.Client.Services;

public class HttpClientService : IHttpClientService
{
    #region Private Properties

    private readonly HttpClient _httpClient;
    private readonly IHttpService _httpService;

    private readonly JsonSerializerOptions _serializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    private JsonSerializerOptions _serializeOptionsNameCase = new JsonSerializerOptions()
        { PropertyNameCaseInsensitive = true };

    #endregion

    #region Lifecycle methods

    /// <summary>
    /// Constructor
    /// </summary>
    public HttpClientService(
        HttpClient httpClient, 
        IHttpService httpService)
    {
        _httpClient = httpClient;
        _httpService = httpService;
        
        httpClient.BaseAddress = new Uri(ServerConstants.WebApiServer);
        httpClient.Timeout = TimeSpan.FromSeconds(5);
        InitHeaders();
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Call Post Async
    /// </summary>
    /// <param name="url"></param>
    /// <param name="req"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public virtual async Task<ApiSuccessResponse<TResponse>> CallPostAsync<TRequest, TResponse>(
        string url, TRequest req)
    {
        var jsonConvert = JsonSerializer.Serialize(req, _serializeOptions);
        StringContent stringContent = new StringContent(jsonConvert, Encoding.UTF8, "application/json");

        return await _httpService.SendAsync<TResponse>(() => _httpClient.PostAsync(url, stringContent));
    }


    /// <summary>
    /// Call Put Async
    /// </summary>
    /// <param name="url"></param>
    /// <param name="req"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public async Task<ApiSuccessResponse<TResponse>> CallPutAsync<TRequest, TResponse>(string url,
        TRequest req)
    {
        return await _httpService.SendAsync<TResponse>(() => _httpClient.PutAsync(url,
            new StringContent(JsonSerializer.Serialize(req, _serializeOptions), Encoding.UTF8, "application/json")));
    }

    /// <summary>
    /// Call put async
    /// </summary>
    /// <param name="url"></param>
    /// <param name="req"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <returns></returns>
    public async Task<HttpStatusCode> CallPutAsync<TRequest>(string url, TRequest req)
    {
        var contentJson = JsonSerializer.Serialize(req, _serializeOptions);
        return await _httpService.SendAsync(() => _httpClient.PutAsync(url, new StringContent(contentJson, Encoding.UTF8, "application/json")));
    }

    /// <summary>
    /// Call Get Async
    /// </summary>
    /// <param name="url"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public async Task<ApiSuccessResponse<TResponse>> CallGetAsync<TResponse>(string url)
    {
        return await _httpService.SendAsync<TResponse>(() => _httpClient.GetAsync(url));
    }

    /// <summary>
    /// Call Delete Async
    /// </summary>
    /// <param name="url"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns> 
    public async Task<ApiSuccessResponse<TResponse>> CallDeleteAsync<TResponse>(string url)
    {
        return await _httpService.SendAsync<TResponse>(() =>  _httpClient.DeleteAsync(url));
    }

    /// <summary>
    /// Call delete async with no return
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<HttpStatusCode> CallDeleteAsync(string url)
    {
        return await _httpService.SendAsync(() =>  _httpClient.DeleteAsync(url));
    }

    #endregion

    #region Private methods

    private void InitHeaders()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    #endregion
}