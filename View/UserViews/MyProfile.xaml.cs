using Services;
using ViewModel.UserViewModels;

namespace View.UserViews;

public partial class MyProfile : ContentPage
{
    private readonly MyProfileViewModel _vm;
    
    public MyProfile(UserServices userServices)
    {
        InitializeComponent();
        _vm = new MyProfileViewModel(userServices);
        BindingContext = _vm;
    }

    private void BackButton_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
