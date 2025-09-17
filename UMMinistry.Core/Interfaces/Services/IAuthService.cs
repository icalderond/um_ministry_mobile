namespace UMMinistry.Core.Interfaces.Services;

public interface IAuthService
{
    /// <summary>
    /// Login Async
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<string> LoginAsync(string userName, string password);
}