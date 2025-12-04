using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Services;

namespace ViewModel.UserViewModels;

public partial class CreatePollViewModel : BaseViewModel
{
    #region Services
    private UserServices _userServices;
    private PollServices _pollServices;
    #endregion
    
    #region View Properties

    [ObservableProperty] private string _title = "";
    [ObservableProperty] private string _description = "";
    [ObservableProperty] private DateTime _deadlineDate = DateTime.Now.AddDays(6);
    [ObservableProperty] private TimeSpan _deadlineTime = TimeSpan.FromHours(23);

    public ObservableCollection<OptionData> Options {get; set;}
    #endregion
    
    #region Commands

    public ICommand AddOptionCommand { get; }
    public ICommand RemoveOptionCommand { get;}
    public ICommand PublishCommand { get; }
    
    #endregion
    
    public CreatePollViewModel(UserServices userServices, PollServices pollServices)
    {
        _userServices = userServices;
        _pollServices = pollServices;
        
    }
    
    #region Command Implementations
    
    
    
    #endregion
}