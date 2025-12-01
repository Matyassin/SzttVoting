namespace Model;

public struct PollModel(UserData user, string title, string description, DateTime deadline)
{
    public UserData Creator = user;
    public string Id = Guid.NewGuid().ToString();
    public string Title = title;
    public string Description = description;
    public DateTime Created = DateTime.Now;
    public DateTime Deadline = deadline;
    public Boolean IsActive = true;
    public List<OptionStruct> Options = new List<OptionStruct>();
    private List<VotesStruct> Votes = new List<VotesStruct>();

}

public struct OptionStruct(string text)
{
    public string Id = Guid.NewGuid().ToString();
    public string Text;
}

public struct VotesStruct
{
    public string Id;
    public UserData Voter;
    public OptionStruct RelatedOption;
    public DateTime Created;
}