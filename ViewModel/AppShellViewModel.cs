using System;
using System.ComponentModel;

namespace ViewModel;
// TODO: Finish Implementing INotifyPrpertyChanged
public class AppShellViewModel: INotifyPropertyChanged
{
    private String _shellTitle = string.Empty;

    public String ShellTitle
    {
        get { return _shellTitle; }
        set { 
            _shellTitle = value; 
            OnPropertyChanged("ShellTitle"); 
        }
    }
    public AppShellViewModel()
    {
        _shellTitle = "Welcome to the ultimate voting system!";
    }
}