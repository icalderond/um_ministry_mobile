using UMMinistry.Core.Interfaces.ViewModels;

namespace UMMinistry.Mobile.UIControls;

public partial class BaseContentPage : ContentPage
{
    public BaseContentPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// When overriden, allows application developers to customize behavior immediately prior to the Page becoming visible
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as IViewModel)?.OnAppearing();
    }

    /// <summary>
    /// When overriden, allows the application developer to customize behavior as the Page disappears
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as IViewModel)?.OnDisappearing();
    }

    /// <summary>
    /// When overriden, allow the application developer customize action when navigation was done
    /// </summary>
    /// <param name="args"></param>
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        (BindingContext as IViewModel)?.OnNavigatedFrom();
    }
}