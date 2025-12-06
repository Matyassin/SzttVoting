using Services;

namespace Tests;

public class UserServicesTests
{
    [Fact]
    public void UserExistsInRepo()
    {
        var userService = new UserServices();
        userService.AddUser("user", "helloka@gmail.com", "pass123");

        Assert.True(userService.ContainsEmail("helloka@gmail.com"));
    }

    [Fact]
    public void SetLoggedInUser()
    {
        var userService = new UserServices();

        userService.AddUser("user", "szia@gmail.com", "pass123");
        userService.SetLoggedInUser("szia@gmail.com");

        Assert.Equal("szia@gmail.com", userService.LoggedInUser.Email);
    }

    [Fact]
    public void ClearLoggedInUser()
    {
        var userService = new UserServices();

        userService.AddUser("user", "puszi@gmail.com", "pass123");
        userService.SetLoggedInUser("puszi@gmail.com");

        userService.ClearLoggedInUser();

        Assert.Null(userService.LoggedInUser);
    }
}
