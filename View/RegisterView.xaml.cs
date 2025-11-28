using ViewModel;

namespace View;

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

    private async void RegisterButton_OnClicked(object sender, EventArgs e)
    {
        if (RegisterButton.IsEnabled)
        {
            _vm.SaveUserCommand.Execute(null);
            await Navigation.PushAsync(new UserView(_vm.EmailEntry));
        }
    }

    private async void ToLoginButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
