using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    public string _emailEntry = "example@example.com";
    
    [ObservableProperty]
    public string _passwordEntry = "";
}