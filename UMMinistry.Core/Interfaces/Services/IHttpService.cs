using System.Net;
using UMMinistry.Core.Models.HttpHelpers;

namespace UMMinistry.Core.Interfaces.Services;

public interface IHttpService
{  
    /// <summary>
    /// Send async with response TResponse
    /// </summary>
    /// <param name="httpAction"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    Task<ApiSuccessResponse<TResponse>> SendAsync<TResponse>(Func<Task<HttpResponseMessage>> httpAction);
    
    /// <summary>
    /// Send async with response HttpStatusCode
    /// </summary>
    /// <param name="httpAction"></param>
    /// <returns></returns>
    Task<HttpStatusCode> SendAsync(Func<Task<HttpResponseMessage>> httpAction);
}