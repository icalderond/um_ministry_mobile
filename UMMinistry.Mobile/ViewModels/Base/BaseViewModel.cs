using CommunityToolkit.Mvvm.ComponentModel;
using UMMinistry.Core.Interfaces.ViewModels;

namespace UMMinistry.Mobile.ViewModels.Base;

public partial class BaseViewModel : ObservableObject, IViewModel
{
    #region Private properties
    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private bool _isTablet;
    [ObservableProperty] private string _mainTitle;
    #endregion

    #region Lifecycle methods
    /// <summary>
    /// Constructor
    /// </summary>
    public BaseViewModel()
    {
        IsTablet = DeviceInfo.Current.Idiom == DeviceIdiom.Tablet;
    }
    #endregion

    #region Virtual methods
    /// <summary>
    /// When overriden, allows application developers to customize behavior immediately prior to the Page becoming visible
    /// </summary>
    public virtual void OnAppearing()
    {
    }

    /// <summary>
    /// When overriden, allows the application developer to customize behavior as the Page disappears
    /// </summary>
    public virtual void OnDisappearing()
    {
    }

    /// <summary>
    /// When overriden, allow the application developer customize action when navigation was done
    /// </summary>
    public virtual void OnNavigatedFrom()
    {
        
    }
    
    /// <summary>
    /// Display alert
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    public async void DisplayAlert(string title, string message)
    {
        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
    #endregion
}