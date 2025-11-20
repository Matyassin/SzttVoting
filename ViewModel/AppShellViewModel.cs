using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;
public partial class AppShellViewModel: BaseViewModel
{
    [ObservableProperty]
    private String _shellTitle = "Welcome to the ultimate voting system!";
}