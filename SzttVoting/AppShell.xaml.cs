using System.ComponentModel;
using ViewModel;

namespace SzttVoting
{
    public partial class AppShell : Shell, INotifyPropertyChanged
    {
        private AppShellViewModel _shellViewModel;
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            _shellViewModel = vm;
        }
    }
}
