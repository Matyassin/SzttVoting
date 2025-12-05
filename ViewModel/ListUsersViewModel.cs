using Services;
using System.Collections.ObjectModel;

namespace ViewModel;

public class ListUsersViewModel : BaseViewModel
{
    public ObservableCollection<UserDisplayModel> Users { get; private set; } = new();
    public UserServices UserServices { get; private set; }

    public ListUsersViewModel(UserServices userServices)
    {
        UserServices = userServices;

        foreach (var user in userServices.Users.Values)
        {
            Users.Add(new UserDisplayModel
            {
                Username = user.Username,
                Email = user.Email,
                IsBlocked = user.IsBlocked
            });
        }
    }
}

// We make this class because Binding requires classes and properties and I refuse to turn structs to classes :)
public class UserDisplayModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsBlocked { get; set; }
}
