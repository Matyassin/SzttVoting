using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace SzttVoting.View;

public partial class RegisterView : ContentPage
{
    private RegisterViewModel _vm;
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