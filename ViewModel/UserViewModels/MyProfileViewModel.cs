using Services;

namespace ViewModel.UserViewModels;

public class MyProfileViewModel(UserServices userServices) : BaseViewModel
{
    public string Username => _userServices.LoggedInUser.Username;
    public string Email => _userServices.LoggedInUser.Email;
    public string Guid => _userServices.LoggedInUser.Guid;
    public string Initials => Username[0].ToString();
    
    private readonly UserServices _userServices = userServices;
}
