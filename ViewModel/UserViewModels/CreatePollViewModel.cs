using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanPublish))]
    private string _title = "";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanPublish))]
    private string _description = "";
    
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(CanPublish))]
    private DateTime _deadlineDate = DateTime.Now.AddDays(6);
    
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(CanPublish))]
    private TimeSpan _deadlineTime = TimeSpan.FromHours(23);
    
    public bool CanPublish => TitleCheck && DescriptionCheck;
    public bool IsDiscardable => String.IsNullOrWhiteSpace(Title) || String.IsNullOrWhiteSpace(Description);
    
    public ObservableCollection<OptionData> Options {get; set;}
    #endregion
    
    public CreatePollViewModel(UserServices userServices, PollServices pollServices)
    {
        _userServices = userServices;
        _pollServices = pollServices;
    }
    
    #region Command Implementations

    [RelayCommand]
    public void AddOption() { }

    [RelayCommand]
    public void RemoveOption() { }
    
    public async void Publish()
    {
        _pollServices.AddPoll(_userServices.LoggedInUser, Title, Description, DeadlineDate + DeadlineTime);
    }
    #endregion
    
    #region Field Check

    private bool TitleCheck => Title.Length < 30 && Title.Length > 0;

    private bool DescriptionCheck => Description.Length < 200 && Description.Length > 0;

    private bool DeadlineCheck()
    {
        if (DeadlineDate + DeadlineTime <= DateTime.Now) return false;
        return true;
    }

    private bool OptionsCheck()
    {
       // TODO - REMOVED FOR TESTING: if (Options.Count < 1) return false;
        return true;
    }
    #endregion
}