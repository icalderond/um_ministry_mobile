using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using UMMinistry.Core.Constants;
using UMMinistry.Core.Interfaces.Services;
using UMMinistry.Core.Models.General;
using UMMinistry.Core.Models.HttpHelpers;

namespace UMMinistry.Core.Services;

public class HttpClientService : IHttpClientService
{
    #region Private Properties

    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _serializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };
    
    private JsonSerializerOptions _serializeOptionsNameCase = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    #endregion

    #region Lifecycle methods

    /// <summary>
    /// Constructor
    /// </summary>
    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        httpClient.BaseAddress = new Uri(ServerConstants.WebApiServer);
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

        var res = await _httpClient.PostAsync(url, stringContent).ConfigureAwait(false);
        return ProcessResponse<TResponse>(res);
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
        var res = await _httpClient.PutAsync(url,
                new StringContent(JsonSerializer.Serialize(req, _serializeOptions), Encoding.UTF8, "application/json"))
            .ConfigureAwait(false);
        return ProcessResponse<TResponse>(res);
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
        var res = await _httpClient.PutAsync(url,
                new StringContent(contentJson, Encoding.UTF8, "application/json"))
            .ConfigureAwait(false);
        return res.StatusCode;
    }

    /// <summary>
    /// Call Get Async
    /// </summary>
    /// <param name="url"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public async Task<ApiSuccessResponse<TResponse>> CallGetAsync<TResponse>(string url)
    {
        var res = await _httpClient.GetAsync(url).ConfigureAwait(false);
        var query = from err in res.Headers.ToList()
            where err.Key == "errorMsg"
            select err.Value;
        return ProcessResponse<TResponse>(res);
    }

    /// <summary>
    /// Call Delete Async
    /// </summary>
    /// <param name="url"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns> 
    public async Task<ApiSuccessResponse<TResponse>> CallDeleteAsync<TResponse>(string url)
    {
        var res = await _httpClient.DeleteAsync(url).ConfigureAwait(false);
        return ProcessResponse<TResponse>(res);
    }

    /// <summary>
    /// Call delete async with no return
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<HttpStatusCode> CallDeleteAsync(string url)
    {
        var res = await _httpClient.DeleteAsync(url).ConfigureAwait(false);
        return res.StatusCode;
    }

    #endregion

    #region Private methods

    private void InitHeaders()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary>
    /// Process Response
    /// </summary>
    /// <param name="res"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    private ApiSuccessResponse<TResponse> ProcessResponse<TResponse>(HttpResponseMessage res)
    {
        var s = res.Content.ReadAsStringAsync().Result;
        if (res.IsSuccessStatusCode)
        {
            var responseSucces = string.IsNullOrEmpty(s)
                ? default
                : JsonSerializer.Deserialize<ApiSuccessResponse<TResponse>>(s,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return responseSucces;
        }

        var responseError = IsValidJson<ApiErrorResponse>(s)
            ? JsonSerializer.Deserialize<ApiErrorResponse>(s, _serializeOptionsNameCase)
            : new ApiErrorResponse() { ErrorMessage = s };

        throw new ApiErrorException(responseError);
    }

    // create method to validate if Json is specific tpe
    private bool IsValidJson<T>(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            // Get all public properties of type T
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Check if all properties exist in the JSON
            foreach (var prop in props)
            {
                if (!root.TryGetProperty(prop.Name, out _))
                {
                    return false; // Missing a property
                }
            }

            return true; // All properties matched
        }
        catch
        {
            return false; // Invalid JSON
        }
    }
    
    private bool IsValidJson(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    #endregion
}