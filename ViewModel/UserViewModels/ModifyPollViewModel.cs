using Model;
using Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel.UserViewModels;

public partial class ModifyPollViewModel : BaseViewModel
{
    private readonly UserServices _userServices;
    private readonly PollServices _pollServices;
    private readonly PollData _pollToChange;

    public ObservableCollection<OptionData> Options { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDiscardable))]
    private string _title = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDiscardable))]
    private string _description = "";

    [ObservableProperty] private DateTime _deadlineDate;
    [ObservableProperty] private TimeSpan _deadlineTime;

    private string _errorMessageTitle = "";
    private string _errorMessageDescription = "";

    public bool IsDiscardable =>
        string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description);


    public ModifyPollViewModel(UserServices userServices, PollServices pollServices, PollData pollData)
    {
        _userServices = userServices;
        _pollServices = pollServices;
        _pollToChange = pollData;

        Title = pollData.Title;
        Description = pollData.Description;
        DeadlineDate = pollData.Deadline.Value.Date;
        DeadlineTime = pollData.Deadline.Value.TimeOfDay;

        Options = new ObservableCollection<OptionData>(pollData.Options);

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

    public async Task<bool> Publish()
    {
        if (CanPublish())
        {
            if (_pollToChange.Title != Title)
            {
                _pollServices.Polls.Remove(_pollToChange.Title);
            }

            _pollToChange.Title = Title;
            _pollToChange.Description = Description;
            _pollToChange.Deadline = DeadlineDate + DeadlineTime;
            _pollToChange.Options = Options.ToList();
            _pollToChange.Created = DateTime.UtcNow;
            _pollServices.Polls[_pollToChange.Title] = _pollToChange;
            _pollServices.Save();

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

        return TitleCheck() && DescriptionCheck() && DeadlineCheck() && OptionsCheck() && UserStatusCheck();
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
}
