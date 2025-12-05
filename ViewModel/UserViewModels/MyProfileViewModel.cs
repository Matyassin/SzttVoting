using Services;

namespace ViewModel.UserViewModels;

public class MyProfileViewModel: BaseViewModel
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Guid { get; private set; }
    public string Initials { get; private set; }

    public UserServices UserServices { get; private set; }

    public MyProfileViewModel(UserServices userServices)
    {
        UserServices = userServices;

        Username = userServices.LoggedInUser.Username;
        Email = userServices.LoggedInUser.Email;
        Guid = userServices.LoggedInUser.Guid;

        Initials = Username[0].ToString();
    }
}
