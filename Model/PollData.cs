using CommunityToolkit.Mvvm.ComponentModel;

namespace Model;

public partial class PollData : ObservableObject
{
    [ObservableProperty] private string _creatorID;
    [ObservableProperty] private string _id;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private DateTime _created;
    [ObservableProperty] private DateTime? _deadline;
    [ObservableProperty] private bool _isActive;
    [ObservableProperty] private List<OptionData> _options;
    [ObservableProperty] private List<VotesData> _votes;
    public PollData(string creeatorGuid, string title, string description, DateTime? deadline, List<OptionData> options, List<VotesData> votes)
    {
        CreatorID = creeatorGuid;
        Id = Guid.NewGuid().ToString();
        Title = title;
        Description = description;
        Created = DateTime.Now;
        Deadline = deadline;
        IsActive = true;
        Options = options;
        Votes = votes;
    }
}

public partial class OptionData(string text) : ObservableObject
{
    [ObservableProperty] private string _id = Guid.NewGuid().ToString();
    [ObservableProperty] private string _text = text;
}

public partial class VotesData(UserData user, OptionData option) : ObservableObject
{
    [ObservableProperty] private string _id;
    [ObservableProperty] private string _voterID = user.Guid;
    [ObservableProperty] private string _relatedOption = option.Id;
    [ObservableProperty] private DateTime _created = DateTime.Now;
}
