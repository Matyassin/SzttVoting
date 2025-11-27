using Newtonsoft.Json;

namespace Model;

public static class UsersRepo
{
    private static Dictionary<string, string> _userProfiles = new();
    private const string _fileName = "userprofiles.json";

    public const string EmailPattern = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";
    public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]+$";

    public static void Save(UserData userToSave)
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

    public static void Load()
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

    public static bool ContainsEmail(string email)
    {
        return _userProfiles.ContainsKey(email);
    }

    public static bool ValidateUser(UserData userToValidate)
    {
        bool emailFound = _userProfiles.TryGetValue(userToValidate.Email, out string? storedPassword);
        bool passwordMatches = userToValidate.Password == storedPassword;

        return emailFound && passwordMatches;
    }
}
