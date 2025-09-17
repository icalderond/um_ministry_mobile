namespace UMMinistry.Core.ViewModels;

public interface IViewModel
{
    /// <summary>
    /// When overriden, allows application developers to customize behavior immediately prior to the Page becoming visible
    /// </summary>
    void OnAppearing();
    
    /// <summary>
    /// When overriden, allows the application developer to customize behavior as the Page disappears
    /// </summary>
    void OnDisappearing();

    /// <summary>
    /// When overriden, allow the application developer customize action when navigation was done
    /// </summary>
    void OnNavigatedFrom();
}