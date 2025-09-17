using UMMinistry.Core.Interfaces.Services;

namespace UMMinistry.Mobile.Utilities.Services;

public class NavigationService : INavigationService
{    
    /// <summary>
    /// Shell GoToAsync
    /// </summary>
    /// <param name="route"></param>
    /// <param name="animate"></param>
    public async Task ShellGoToAsync(string route = null, bool animate = true)
    {
        await Shell.Current.GoToAsync(route, animate);
    }

    /// <summary>
    /// Pop to root async
    /// </summary>
    /// <param name="animate"></param>
    public async Task PopToRootAsync(bool animate = true)
    {
        await Application.Current.MainPage.Navigation.PopToRootAsync();
    }
}