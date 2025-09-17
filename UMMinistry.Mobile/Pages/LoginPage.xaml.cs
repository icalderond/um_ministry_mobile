using UMMinistry.Core.Interfaces.ViewModels;

namespace UMMinistry.Mobile.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(ILoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}