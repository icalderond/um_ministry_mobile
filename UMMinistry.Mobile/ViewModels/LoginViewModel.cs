using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UMMinistry.Core.Interfaces.Services;
using UMMinistry.Core.Interfaces.ViewModels;
using UMMinistry.Mobile.ViewModels.Base;

namespace UMMinistry.Mobile.ViewModels;

public partial class LoginViewModel : BaseViewModel, ILoginViewModel
{
    #region Private Properties
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;
    [ObservableProperty] private string _userName;
    [ObservableProperty] private string _password;
    #endregion

    #region Lifecycle Methods
    public LoginViewModel(IAuthService authService, INavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }
    #endregion
    
    #region Public Methods
    [RelayCommand]
    public async Task LoginAsync()
    {
        var response = await _authService.LoginAsync(UserName, Password);
        var token = response;
    }   
    #endregion
}