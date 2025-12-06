using Services;

namespace ViewModel;

public class AdminViewModel : BaseViewModel
{
    public UserServices UserServices { get; private set; }
    public PollServices PollServices { get; private set; }

    public string Username { get; private set; }

    public AdminViewModel(UserServices userServices)
    {
        UserServices = userServices;
        PollServices = new PollServices();

        Username = userServices.LoggedInUser.Username;

        PollServices.Load();
    }
}
