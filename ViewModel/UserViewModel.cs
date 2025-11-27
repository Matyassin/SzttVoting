using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class UserViewModel : BaseViewModel
{
    [ObservableProperty] private string _userEmail;
    [ObservableProperty] private string _headerMainText;
    public UserViewModel(string email)
    {
        UserEmail = email;
        HeaderMainText = "Welcome " + _userEmail;
    }
}
