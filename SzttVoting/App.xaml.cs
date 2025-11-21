using Model;
using SzttVoting.View;

namespace SzttVoting;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        UsersRepo.Load();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new LoginView());
    }
}