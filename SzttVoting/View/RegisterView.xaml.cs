using ViewModel;

namespace SzttVoting.View;

public partial class RegisterView : ContentPage
{
    private RegisterViewModel _vm;
    public RegisterView()
    {
        InitializeComponent();
        _vm = new RegisterViewModel();
        BindingContext = _vm;
        
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginView();
    }
}