using System.Net;
using UMMinistry.Core.Constants;
using UMMinistry.Core.Interfaces.Services;

namespace UMMinistry.Client;

public class AuthService : IAuthService
{
    private readonly IHttpClientService _httpClientService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpClientService"></param>
    public AuthService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    /// <summary>
    /// Login Async
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<string> LoginAsync(string userName, string password)
    {
        var apiUrl = ApiConstants.UrlBase
                     + ApiConstants.AuthControllerName
                     + ApiConstants.LoginMethodName;
        
        var response = await _httpClientService.CallPostAsync<object, String>(apiUrl, new { userName, password });
        return (HttpStatusCode)response.HttpCode != HttpStatusCode.OK 
            ? throw new Exception(response.Message) 
            : response.Data;
    }
}