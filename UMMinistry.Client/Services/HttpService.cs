using System.Net;
using System.Reflection;
using System.Text.Json;
using UMMinistry.Core.Extensions;
using UMMinistry.Core.Interfaces.Services;
using UMMinistry.Core.Models.General;
using UMMinistry.Core.Models.HttpHelpers;

namespace UMMinistry.Client.Services;

public class HttpService : IHttpService
{
    private readonly JsonSerializerOptions _serializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    private JsonSerializerOptions _serializeOptionsNameCase = new JsonSerializerOptions()
        { PropertyNameCaseInsensitive = true };


    /// <summary>
    /// Send async with response TResponse
    /// </summary>
    /// <param name="httpAction"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    /// <exception cref="ApiErrorException"></exception>
    public async Task<ApiSuccessResponse<TResponse>> SendAsync<TResponse>(Func<Task<HttpResponseMessage>> httpAction)
    {
        try
        {
            using var response = await httpAction();
            return ProcessResponse<TResponse>(response);
        }
        catch (TaskCanceledException ex)
        {
            throw GetThrownError(ex.Message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            throw GetThrownError(ex.Message, ex.StatusCode.Value);
        }
        catch (Exception ex)
        {
            throw GetThrownError(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Send async with response HttpStatusCode
    /// </summary>
    /// <param name="httpAction"></param>
    /// <returns></returns>
    /// <exception cref="ApiErrorException"></exception>
    public async Task<HttpStatusCode> SendAsync(Func<Task<HttpResponseMessage>> httpAction)
    {
        try
        {
            using var response = await httpAction();
            return response.StatusCode;
        }
        catch (TaskCanceledException ex)
        {
            throw GetThrownError(ex.Message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            throw GetThrownError(ex.Message, ex.StatusCode.Value);
        }
        catch (Exception ex)
        {
            throw GetThrownError(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Process Response
    /// </summary>
    /// <param name="res"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    private ApiSuccessResponse<TResponse>? ProcessResponse<TResponse>(HttpResponseMessage res)
    {
        var responseSucces = new ApiSuccessResponse<TResponse>
        {
            HttpCode = (int)res.StatusCode
        };

        var s = res.Content.ReadAsStringAsync().Result;
        if (!res.IsSuccessStatusCode) throw GetThrownError(s, res.StatusCode);
        if (string.IsNullOrEmpty(s)) return  null;
        
        if (typeof(TResponse) == typeof(string))
        {
            responseSucces.Data = (TResponse)(object)s;
        }
        else
        {
            responseSucces = JsonSerializer.Deserialize<ApiSuccessResponse<TResponse>>(s,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        return responseSucces;
    }

    /// <summary>
    /// Get thrown error
    /// </summary>
    /// <param name="s"></param>
    /// <param name="httpStatusCode"></param>
    /// <returns></returns>
    private ApiErrorException GetThrownError(string s, HttpStatusCode httpStatusCode)
    {
        var responseError = s.IsValidJson<ApiErrorResponse>()
            ? JsonSerializer.Deserialize<ApiErrorResponse?>(s, _serializeOptionsNameCase)
            : new ApiErrorResponse() { ErrorMessage = s, HttpCode = (int)httpStatusCode };

        return new ApiErrorException(new ApiErrorResponse());
    }
}