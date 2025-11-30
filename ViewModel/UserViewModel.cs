using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace ViewModel;

public partial class UserViewModel : BaseViewModel
{
    public string UserEmail => _userServices.LoggedInUser.Email;
    public string UserName => _userServices.LoggedInUser.Username;
    public string HeaderMainText => $"Welcome, {UserName}!";
    
    private readonly UserServices _userServices;

    public UserViewModel(UserServices userServices)
    {
        _userServices = userServices;
    }
}
