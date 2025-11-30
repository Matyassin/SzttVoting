using Newtonsoft.Json;

namespace Model;

public class UserData(string username,string email, string password)
{
    [JsonProperty("username")] public string Username = username;
    
    [JsonProperty("email")] public string Email = email;

    [JsonProperty("password")] public string Password = password;
}