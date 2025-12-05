using Services;

namespace ViewModel;

public class UserViewModel : BaseViewModel
{
    public string UserEmail { get; private set; }
    public string UserName { get; private set; }
    public string HeaderMainText { get; private set; }

    public UserServices UserServices { get; private set; }
    
    public PollServices PollServices { get; private set; }

    public UserViewModel(UserServices userServices)
    {
        UserServices = userServices;
        PollServices = new PollServices();

        UserEmail = UserServices.LoggedInUser.Email;
        UserName = UserServices.LoggedInUser.Username;
        HeaderMainText = $"Welcome, {UserName}!";
    }
}
