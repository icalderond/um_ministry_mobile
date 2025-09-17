using UMMinistry.Core.Interfaces.ViewModels;
using UMMinistry.Mobile.UIControls;

namespace UMMinistry.Mobile.Pages;

public partial class LoginPage : BaseContentPage
{
    public LoginPage(ILoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}