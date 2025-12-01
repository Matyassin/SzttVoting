using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Model;

namespace Services;

public class UserServices
{
    private Dictionary<string, UserData> _userProfiles = new();
    private string _fileName = "userprofiles.json";
    public UserData LoggedInUser { get; private set; }
    
    #region User login
    public void SetLoggedInUser(string email)
    {
        LoggedInUser = GetUserFromEmail(email);
    }
    
    public void ClearLoggedInUser(){ LoggedInUser = default; }
    
    #endregion

    #region User data management from files

    public void AddUser(string username, string email, string password)
    {
        _userProfiles.Add(email, CreateUserDataObj(username,email,password));
        Save();
    }
    
    public void Save()
    {
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
        _userProfiles = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json);
    }

    public bool ContainsEmail(string email)
    {
        return _userProfiles.ContainsKey(email);
    }
    #endregion

    #region User Utils
    
     public bool ValidateUser(string emailToBeValidated, string password)
    {
        var isUserFound = _userProfiles.TryGetValue(emailToBeValidated, out UserData user);
        if (!isUserFound) return false;
        
        var foundUser = _userProfiles[emailToBeValidated];
        
        bool passwordMatches = CryptographyServices.IsPasswordValid(password, foundUser.Password);

        return isUserFound && passwordMatches;
    }

    private UserData CreateUserDataObj(string username, string email, string password)
    {
        return new UserData(Guid.NewGuid().ToString(), username, email, CryptographyServices.HashPassword(password));
    }
    private UserData GetUserFromEmail(string email) { return _userProfiles[email]; }
    #endregion
    
}
