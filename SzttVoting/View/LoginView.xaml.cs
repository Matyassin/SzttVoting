using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SzttVoting.View
{
    public partial class LoginView : ContentView, INotifyPropertyChanged
    {
        private string _emailEntry = "example@example.com";
        private string _passwordEntry = "";
        public LoginView()
        {
            InitializeComponent();
            BindingContext = this;
        }
        
        public string EmailEntry
        {
            get => _emailEntry;
            set
            {
                if (_emailEntry != value)
                {
                    _emailEntry = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public string PasswordEntry
        {
            get => _passwordEntry;
            set
            {
                if (_passwordEntry != value)
                {
                    _passwordEntry = value;
                    OnPropertyChanged(); 
                }
            }
        }
        
    }
}
