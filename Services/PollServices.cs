using Model;
using Newtonsoft.Json;

namespace Services;


public class PollServices : IDataService
{
    private string _fileName = "polldata.json";
    private Dictionary<string, Poll> _allPolls = new Dictionary<string, Poll>();


    public void AddPoll(UserData currUser,string title, string desc, DateTime deadline)
    {
        _allPolls.Add(title, new Poll(currUser, title, desc, deadline));
        Save();
    }
    public void Save()
    {
        string json = JsonConvert.SerializeObject(_allPolls, Formatting.Indented);
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
        _allPolls = JsonConvert.DeserializeObject<Dictionary<string, Poll>>(json);
    }
}