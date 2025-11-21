using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
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
        {
            _vm.IsEmailError = true;
            _vm.IsPasswordError = true;
        }
    }
}
