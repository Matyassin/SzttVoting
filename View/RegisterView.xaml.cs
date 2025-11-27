using ViewModel;

namespace SzttVoting.View;

public partial class RegisterView : ContentPage
{
    private readonly RegisterViewModel _vm;

    public RegisterView()
    {
        InitializeComponent();
        _vm = new RegisterViewModel();
        BindingContext = _vm;
    }
    
    private void Email_OnUnfocused(object? sender, FocusEventArgs e)
    {
        _vm.CheckEmailEntryCommand.Execute(null);
    }
        
    private void Password_OnUnfocused(object? sender, FocusEventArgs e)
    {
        _vm.CheckPasswordEntryCommand.Execute(null);
    }

    private void RegisterButton_OnClicked(object sender, EventArgs e)
    {
        _vm.SaveUserCommand.Execute(null);
        Application.Current.MainPage = new UserView();
    }

    private void ToLoginButton_OnClicked(object? sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginView();
    }
}
