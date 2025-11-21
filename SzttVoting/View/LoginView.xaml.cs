using ViewModel;

namespace SzttVoting.View
{
    public partial class LoginView : ContentPage
    {
        private LoginViewModel _vm;
        public LoginView()
        {
            InitializeComponent();
            _vm = new LoginViewModel();
            BindingContext = _vm;
        }
        
        private void ToRegisterButton_OnClicked(object? sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterView();
        }

        private void LoginButton_OnClicked(object? sender, EventArgs e)
        { }

        private void Email_OnUnfocused(object? sender, FocusEventArgs e)
        {
            _vm.CheckEmailEntryCommand.Execute(null);
        }
        
        private void Password_OnUnfocused(object? sender, FocusEventArgs e)
        {
            _vm.CheckPasswordEntryCommand.Execute(null);
        }
    }
}
