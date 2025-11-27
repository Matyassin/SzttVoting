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
        Assert.False(vm.IsEmailValid("terminator@valamiponthu"));
    }

    [Fact]
    public void EmailWithOnlyOneTLDChr()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsEmailValid("hubbabubba@gmail.c"));
    }

    [Fact]
    public void EmailIsCorrect()
    {
        var vm = new RegisterViewModel();
        Assert.True(vm.IsEmailValid("metin2@citromail.com"));
    }

    [Fact]
    public void PasswordLengthLessThanFiveCharacters()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsPasswordValid("Wsd1"));
    }

    [Fact]
    public void PasswordContainsSymbol()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsPasswordValid("Wasd1@{"));
    }

    [Fact]
    public void PasswordContainsOnlyLowercaseCharacters()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsPasswordValid("wasdf1"));
    }

    [Fact]
    public void PasswordWithNoNumbers()
    {
        var vm = new RegisterViewModel();
        Assert.False(vm.IsPasswordValid("Wasdfg"));
    }

    [Fact]
    public void PasswordIsCorrect()
    {
        var vm = new RegisterViewModel();
        Assert.True(vm.IsPasswordValid("Halflife3when"));
    }
}
