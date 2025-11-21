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
        
        private void Button_OnClicked(object? sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterView();
        }
    }
}
