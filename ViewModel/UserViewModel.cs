using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class UserViewModel : BaseViewModel
{
    [ObservableProperty] private string _userEmail;

    public UserViewModel(string email)
    {
        UserEmail = email;
    }
}
