using UMMinistry.Client;
using UMMinistry.Core.Interfaces.Services;
using UMMinistry.Core.Interfaces.ViewModels;
using UMMinistry.Core.Services;
using UMMinistry.Core.ViewModels;
using UMMinistry.Mobile.Pages;
using UMMinistry.Mobile.Utilities.Services;
using UMMinistry.Mobile.ViewModels;

namespace UMMinistry.Mobile.Utilities.Extensions;

public static class MauiAppExtensions
{
     /// <summary>
    /// Register Pages
    /// </summary>
    /// <param name="mauiAppBuilder"></param>
    /// <returns></returns>
    public static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<AppShell>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddSingleton<MeetingDaysPage>();

        return mauiAppBuilder;
    }

    /// <summary>
    /// Register ViewModels
    /// </summary>
    /// <param name="mauiAppBuilder"></param>
    /// <returns></returns>
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
         mauiAppBuilder.Services.AddTransient<IAppShellViewModel, AppShellViewModel>();
         mauiAppBuilder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
         mauiAppBuilder.Services.AddTransient<IMeetingDaysViewModel, MeetingDaysViewModel>();
        
        return mauiAppBuilder;
    }

    /// <summary>
    /// Register Services
    /// </summary>
    /// <param name="mauiAppBuilder"></param>
    /// <returns></returns>
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IHttpClientService, HttpClientService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        mauiAppBuilder.Services.AddSingleton<HttpClient>();
        mauiAppBuilder.Services.AddSingleton<IAuthService, AuthService>();
        
        return mauiAppBuilder;
    }

    /// <summary>
    /// Register Shell Routes
    /// </summary>
    /// <param name="mauiAppBuilder"></param>
    public static MauiAppBuilder RegisterShellRoutes(this MauiAppBuilder mauiAppBuilder)
    {
        Routing.RegisterRoute(nameof(AppShellViewModel), typeof(AppShell));
        Routing.RegisterRoute(nameof(LoginViewModel), typeof(LoginPage));
        Routing.RegisterRoute(nameof(MeetingDaysViewModel), typeof(MeetingDaysPage));

        return mauiAppBuilder;
    }
}