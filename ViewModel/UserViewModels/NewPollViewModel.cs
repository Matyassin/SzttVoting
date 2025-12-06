using Model;
using Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel.UserViewModels;

public partial class NewPollViewModel : BaseViewModel
{
    private readonly UserServices _userServices;
    private readonly PollServices _pollServices;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDiscardable))]
    private string _title = "";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDiscardable))]
    private string _description = "";
    
    [ObservableProperty] 
    private DateTime _deadlineDate = DateTime.Now.AddDays(6);
    
    [ObservableProperty] 
    private TimeSpan _deadlineTime = TimeSpan.FromHours(23);

    private string _errorMessageTitle = "";
    private string _errorMessageDescription = "";

    public ObservableCollection<OptionData> Options { get; set; }

    public bool IsDiscardable =>
        string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description);

    
    public NewPollViewModel(UserServices userServices, PollServices pollServices)
    {
        _userServices = userServices;
        _pollServices = pollServices;
        Options = new ObservableCollection<OptionData>();
        
        Options.CollectionChanged += (sender, e) => 
        {
            OnPropertyChanged(nameof(CanPublish));
        };
        
        OnPropertyChanged(nameof(CanPublish));
    }
    
    [RelayCommand]
    public async Task AddOption()
    {
        if (Application.Current?.MainPage == null)
            return;
        
        string result = await Application.Current.MainPage.DisplayPromptAsync(
                        "Add Option",
                        "Enter the option text:",
                        accept: "Add",
                        cancel: "Cancel");
        
        if (string.IsNullOrWhiteSpace(result))
            return;
        
        Options.Add(new OptionData(result));
        OnPropertyChanged(nameof(CanPublish));
    }

    [RelayCommand]
    public void RemoveOption(OptionData option)
    {
        if (Options.Contains(option))
        {
            Options.Remove(option);
        }

        OnPropertyChanged(nameof(CanPublish));
    }
    
    //Because of the UI changes this has to be a method, which is called from the code behind,
    //to be able to display a DisplayAlert and to tell the Navigation class to go back.
    public async Task<bool> Publish()
    {
        if (CanPublish())
        {
            _pollServices.AddPoll(_userServices.LoggedInUser,
                Title,
                Description,
                DeadlineDate + DeadlineTime,
                Options.ToList(),
                new List<VotesData>()
            );

            return true;
        }

        await Application.Current.MainPage.DisplayAlert(
            _errorMessageTitle, 
            _errorMessageDescription, 
            cancel: "OK");

        return false;
    }
    
    private bool CanPublish()
    {
        _errorMessageDescription = "";
        _errorMessageTitle = "";

        return TitleCheck() && DoesAlreadyExist() && DescriptionCheck() && DeadlineCheck() && OptionsCheck() && UserStatusCheck();
    }

    private bool TitleCheck()
    {
        if (Title.Length > 0 && Title.Length < 50)
            return true;

        _errorMessageTitle = "Check the title entry!";
        _errorMessageDescription = "The title must be between 1 and 49 characters long!";

        return false;
    }

    private bool DescriptionCheck()
    {
        if (Description.Length < 200 && Description.Length > 0) 
            return true;

        _errorMessageTitle = "Check the description entry!";
        _errorMessageDescription = "The description must be between 1 and 199 characters long!";

        return false;
    }

    private bool DeadlineCheck()
    {
        if (DeadlineDate.Date + DeadlineTime >= DateTime.Now.AddHours(1)) 
            return true;

        _errorMessageTitle = "Check the time / date!";
        _errorMessageDescription = "The deadline must at least be 1 hour in the future!";

        return false;
    }

    private bool OptionsCheck()
    {
        if (Options.Count > 1 && Options.Count < 10)
            return true;

        _errorMessageTitle = "Check the options!";
        _errorMessageDescription = "Must have at least 2, and at maximum 9 options!";

        return false;
    }

    private bool UserStatusCheck()
    {
        if (!_userServices.LoggedInUser.IsBlocked)
            return true;

        _errorMessageTitle = "Somebody was a naughty boy! UwU ;)";
        _errorMessageDescription = "Contact an administrator about this privilege restriction!";

        return false;
    }

    private bool DoesAlreadyExist()
    {
        if (!_pollServices.Polls.ContainsKey(Title))
            return true;

        _errorMessageTitle = "This title already exists!";
        _errorMessageDescription = "Please try again with a different title!";

        return false;
    }
}
