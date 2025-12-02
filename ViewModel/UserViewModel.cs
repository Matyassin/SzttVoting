using Services;

namespace ViewModel;

public class UserViewModel(UserServices userServices) : BaseViewModel
{
    public string UserEmail => _userServices.LoggedInUser.Email;
    public string UserName => _userServices.LoggedInUser.Username;
    public string HeaderMainText => $"Welcome, {UserName}!";
    
    private readonly UserServices _userServices = userServices;
}
