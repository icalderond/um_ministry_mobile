using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMMinistry.Core.Interfaces.ViewModels;
using UMMinistry.Mobile.UIControls;

namespace UMMinistry.Mobile.Pages;

public partial class MeetingDaysPage : BaseContentPage
{
    public MeetingDaysPage(IMeetingDaysViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}