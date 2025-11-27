using ViewModel;

namespace Tests;

public class InputValidationTests
{
    [Fact]
    public void EmailEmpty()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsEmailValid(""));
    }

    [Fact]
    public void EmailWithoutAtSign()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsEmailValid("hubbabubba.gmail.com"));
    }

    [Fact]
    public void EmailWithoutDot()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsEmailValid("terminatorponthu"));
    }

    [Fact]
    public void EmailWithOnlyOneTLDChr()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsEmailValid("hubbabubba@gmail.c"));
    }

    [Fact]
    public void PasswordLengthLessThanFiveCharacters()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsPasswordValid("Wasd"));
    }

    [Fact]
    public void PasswordContainsSymbol()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsPasswordValid("Wasd@{"));
    }

    [Fact]
    public void PasswordContainsOnlyLowercaseCharacters()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsPasswordValid("wasdf"));
    }
}
