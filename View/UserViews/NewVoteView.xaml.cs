using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace View.UserViews;

public partial class NewVoteView : ContentPage
{
    private UserServices _userServices;
    public NewVoteView(UserServices _userServices)
    {
        InitializeComponent();
    }

    private void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}