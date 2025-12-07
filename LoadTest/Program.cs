using Services;
using System.Diagnostics;

namespace LoadTest;

public class Program
{
    public static void Main(string[] args)
    {
        string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "large_userprofiles.json");

        // ------ Adjust these for Load Testing ------
        int userCount = 100;
        int userToFindIndex = 50;
        // --------------------------------------------

        UserServices userService = new(filePath);

        Console.WriteLine($"Started generating {userCount} users");
        var sw = Stopwatch.StartNew();

        for (int i = 0; i < userCount; i++)
        {
            userService.AddUser($"User{i}", $"user{i}@example.com", "Password1");
        }

        sw.Stop();

        Console.WriteLine($"Finished generating users in {sw.ElapsedMilliseconds / 1000.0f} s");
        Console.WriteLine($"Looking up: user{userToFindIndex}@example.com");

        sw.Restart();
        bool foundUser = userService.Users.TryGetValue($"user{userToFindIndex}@example.com", out _);
        sw.Stop();

        Console.WriteLine(foundUser
            ? $"Lookup took {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms)"
            : "User not found.");

        File.Delete(filePath);
    }
}
