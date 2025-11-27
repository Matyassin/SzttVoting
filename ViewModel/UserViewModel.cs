using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class UserViewModel : BaseViewModel
{
    [ObservableProperty] private string _userEmail;
    [ObservableProperty] private string _headerMainText;

    public UserViewModel(string email)
    {
        UserEmail = email;

        string emailFirstPart = UserEmail.Split('@').First();
        string userName = char.ToUpper(emailFirstPart[0]) + emailFirstPart.Substring(1);

        HeaderMainText = $"Welcome, {userName}!";
    }
}
