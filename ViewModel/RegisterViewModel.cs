using Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel;

public partial class RegisterViewModel: BaseViewModel
{
    [ObservableProperty]
    private string _emailEntry = "example@example.com";
    
    [ObservableProperty]
    private string _passwordEntry = "";

    [RelayCommand]
    private void SaveUser()
    {
        UsersRepo.Save(new UserProfile(EmailEntry, PasswordEntry));
    }
}