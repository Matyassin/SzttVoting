using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using ViewModel;

namespace SzttVoting.View
{
    public partial class LoginView : ContentView, INotifyPropertyChanged
    {
        private LoginViewModel _viewModel;
        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            BindingContext = this;
        }
        
        public string EmailEntry
        {
            get => _viewModel.EmailEntry;
            set
            {
                if (_viewModel.EmailEntry != value)
                {
                    _viewModel.EmailEntry = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public string PasswordEntry
        {
            get => _viewModel.PasswordEntry;
            set
            {
                if (_viewModel.PasswordEntry != value)
                {
                    _viewModel.PasswordEntry = value;
                    OnPropertyChanged(); 
                }
            }
        }
        
    }
}
