using Newtonsoft.Json;
using Model;

namespace Services;

public  class UserServices
{
    private Dictionary<string, string> _userProfiles = new();
    private string _fileName = "userprofiles.json";
    public UserData LoggedInUser { get; private set; }

    #region User login
    public void SetLoggedInUser(UserData loggedInUser) { LoggedInUser = loggedInUser; }
    
    public void ClearLoggedInUser(){ LoggedInUser = default; }
    
    #endregion

    #region User data management from files
    public void Save(UserData userToSave)
    {
        _userProfiles.Add(userToSave.Email, userToSave.Password);

        string json = JsonConvert.SerializeObject(_userProfiles, Formatting.Indented);
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
        _userProfiles = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    }

    public bool ContainsEmail(string email)
    {
        return _userProfiles.ContainsKey(email);
    }
    #endregion

    public bool ValidateUser(UserData userToValidate)
    {
        bool emailFound = _userProfiles.TryGetValue(userToValidate.Email, out string? storedPassword);
        bool passwordMatches = userToValidate.Password == storedPassword;

        return emailFound && passwordMatches;
    }
    
}
