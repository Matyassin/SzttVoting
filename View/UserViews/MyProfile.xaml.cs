using Services;

namespace View.UserViews;

public partial class MyProfile : ContentPage
{
    private UserServices _userServices;
    
    public MyProfile(UserServices userServices)
    {
        InitializeComponent();
        _userServices = userServices;
    }
}