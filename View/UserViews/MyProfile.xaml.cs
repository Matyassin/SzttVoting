using Services;
using ViewModel;
using ViewModel.UserViewModels;

namespace View.UserViews;

public partial class MyProfile : ContentPage
{
    private UserServices _userServices;
    private MyProfileViewModel _vm;
    
    public MyProfile(UserServices userServices)
    {
        InitializeComponent();
        _userServices = userServices;
        _vm = new MyProfileViewModel(_userServices);
        BindingContext = _vm;
    }

    private void BackButton_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}