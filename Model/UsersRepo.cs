using Newtonsoft.Json;

namespace Model;

public static class UsersRepo
{
    private static List<UserProfile> _userProfiles = new();
    private const string _fileName = "userprofiles.json";

    public static void Save(UserProfile userToSave)
    {
        _userProfiles.Add(userToSave);

        string json = JsonConvert.SerializeObject(_userProfiles, Formatting.Indented);

        string currentDir = Directory.GetCurrentDirectory();
        string slnPath = Path.Combine(Directory.GetParent(currentDir).Parent.Parent.Parent.Parent.FullName, _fileName);

        File.WriteAllText(slnPath, json);
    }

    public static void Load()
    {
        string currentDir = Directory.GetCurrentDirectory();
        string slnPath = Path.Combine(Directory.GetParent(currentDir).Parent.Parent.Parent.Parent.FullName, _fileName);

        if (!File.Exists(slnPath))
            return;

        string json = File.ReadAllText(slnPath);
        _userProfiles = JsonConvert.DeserializeObject<List<UserProfile>>(json);
    }

    public static bool ContainsEmail(string email)
    {
        foreach (var user in _userProfiles)
        {
            if (user.Email == email)
            {
                return true;
            }
        }
        return false;
    }

    public static bool ValidateUser(UserProfile userToValidate)
    {
        foreach (var user in _userProfiles)
        {
            if (user.Email == userToValidate.Email && user.Password == userToValidate.Password)
            {
                return true;
            }
        }

        return false;
    }
}
