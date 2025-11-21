using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using ViewModel;

namespace SzttVoting.View
{
    public partial class LoginView : ContentPage, INotifyPropertyChanged
    {
        private LoginViewModel _vm;
        public LoginView()
        {
            InitializeComponent();
            _vm = new LoginViewModel();
            BindingContext = _vm;
        }
        
        public string EmailEntry
        {
            get => _vm.EmailEntry;
            set
            {
                if (_vm.EmailEntry != value)
                {
                    _vm.EmailEntry = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public string PasswordEntry
        {
            get => _vm.PasswordEntry;
            set
            {
                if (_vm.PasswordEntry != value)
                {
                    _vm.PasswordEntry = value;
                    OnPropertyChanged(); 
                }
            }
        }

        private void Button_OnClicked(object? sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterView();
        }
    }
}
