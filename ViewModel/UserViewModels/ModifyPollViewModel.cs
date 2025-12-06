using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Services;

namespace ViewModel.UserViewModels;

public partial class ModifyPollViewModel : NewPollViewModel
{

    private PollData _pollToChange;

    public ObservableCollection<OptionData> Options { get; set; }
    
    
    public ModifyPollViewModel(UserServices userServices, PollServices pollServices, PollData pollData) 
        : base(userServices, pollServices)
    {
        Options = new ObservableCollection<OptionData>(pollData.Options);
        Title = pollData.Title;
        Description = pollData.Description;
        var tempDate = pollData.Deadline.Value;
        DeadlineDate = tempDate.Date;
        DeadlineTime = tempDate.TimeOfDay;
    }
    
    public new async Task<bool> Publish()
    {
        if (CanPublish()){
            _pollServices.Polls.Remove(_pollToChange.Title);
            _pollToChange.Title = Title;
            _pollToChange.Description = Description;
            _pollToChange.Deadline = DeadlineDate + DeadlineTime;
            _pollToChange.Options = Options.ToList();
            _pollToChange.Votes = new List<VotesData>();
            _pollToChange.Created = DateTime.UtcNow;
            _pollServices.Polls.Add(_pollToChange.Title, _pollToChange);
            _pollServices.Save();
            return true;
        }
        await Application.Current.MainPage.DisplayAlert(
            _errorMessageTitle, 
            _errorMessageDescription, 
            cancel: "OK");
        return false;
    }
    
    public bool CanPublish()
    {
        _errorMessageDescription = "";
        _errorMessageTitle = "";

        return TitleCheck() && DoesAlreadyExist() && DescriptionCheck() && DeadlineCheck() &&OptionsCheck() && UserStatusCheck();
    }
    
    private new bool TitleCheck()
    {
        if ((Title.Length > 0 && Title.Length < 50) || !Title.Equals(_pollToChange.Title)){
            return true;
        }
        _errorMessageTitle = "Check the title entry!";
        _errorMessageDescription = "The title must be between 1 and 49 characters long!";
        return false;
    }
}
