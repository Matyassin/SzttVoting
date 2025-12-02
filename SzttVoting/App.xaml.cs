using View;
using Services;

namespace SzttVoting;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        var userService = new UserServices();
        userService.Load();
        MainPage = new NavigationPage(new LoginView(userService));
    }
}