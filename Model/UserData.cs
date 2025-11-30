using Newtonsoft.Json;

namespace Model;

public struct UserData(string id, string username,string email, string password)
{
    [JsonProperty("id")] public string Guid = id;
    
    [JsonProperty("username")] public string Username = username;
    
    [JsonProperty("email")] public string Email = email;

    [JsonProperty("password")] public string Password = password;
}