using Model;
using Newtonsoft.Json;

namespace Services;

public class PollServices : IDataService
{
    public Dictionary<string, PollData> Polls { get; private set; } = new();
    private readonly string _fileName = "polldata.json";

    public void AddPoll(UserData currUser, string title, string desc, DateTime deadline, List<OptionData> options, List<VotesData> votes)
    {
        Polls.Add(title,
            new PollData(currUser.Guid,
                title,
                desc,
                deadline,
                options,
                votes
            )
        );

        Save();
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(Polls, Formatting.Indented);
        string? slnPath = Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent?
            .Parent?
            .Parent?
            .Parent?
            .FullName;

        string filePath = Path.Combine(slnPath, _fileName);
        File.WriteAllText(filePath, json);
    }

    public void Load()
    {
        string? slnPath = Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent?
            .Parent?
            .Parent?
            .Parent?
            .FullName;

        string filePath = Path.Combine(slnPath, _fileName);

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "{}");
            return;
        }

        string json = File.ReadAllText(filePath);
        Polls = JsonConvert.DeserializeObject<Dictionary<string, PollData>>(json) ?? new Dictionary<string, PollData>();
    }
}
