namespace UMMinistry.Core.Interfaces.Services;

public interface INavigationService
{
    /// <summary>
    /// Shell Go To Async
    /// </summary>
    /// <param name="route"></param>
    /// <param name="animate"></param>
    /// <returns></returns>
    Task ShellGoToAsync(string route = null, bool animate = true);

    /// <summary>
    /// Pop to root async
    /// </summary>
    /// <param name="animate"></param>
    Task PopToRootAsync(bool animate = true);
}