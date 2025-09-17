using System.Net;
using UMMinistry.Core.Models.HttpHelpers;

namespace UMMinistry.Core.Interfaces.Services;

public interface IHttpClientService
{
    /// <summary>
    /// Call Post Async
    /// </summary>
    /// <param name="url"></param>
    /// <param name="req"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    Task<ApiSuccessResponse<TResponse>> CallPostAsync<TRequest, TResponse>(string url, TRequest req);

    /// <summary>
    /// Call Put Async
    /// </summary>
    /// <param name="url"></param>
    /// <param name="req"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public Task<ApiSuccessResponse<TResponse>> CallPutAsync<TRequest, TResponse>(string url, TRequest req);

    /// <summary>
    /// Call put async
    /// </summary>
    /// <param name="url"></param>
    /// <param name="req"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <returns></returns>
    public Task<HttpStatusCode> CallPutAsync<TRequest>(string url, TRequest req);

    /// <summary>
    /// Call Get Async
    /// </summary>
    /// <param name="url"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public Task<ApiSuccessResponse<TResponse>> CallGetAsync<TResponse>(string url);

    /// <summary>
    /// Call Delete Async
    /// </summary>
    /// <param name="url"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns> 
    public Task<ApiSuccessResponse<TResponse>> CallDeleteAsync<TResponse>(string url);

    /// <summary>
    /// Call delete async with no return
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public Task<HttpStatusCode> CallDeleteAsync(string url);
}