using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using ViewModel.UserViewModels;

namespace View.UserViews;

public partial class NewPollView : ContentPage
{
    private UserServices _userServices;
    private CreatePollViewModel _vm;
    public NewPollView(UserServices _userServices)
    {
        InitializeComponent();
    }

    private void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}