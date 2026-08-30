using BankingApplication.entity;
using BankingApplication.repository;
using BankingApplication.service.impl;

namespace BankingApplication.Tests;

/// <summary>Tests for registration and login in <see cref="UserService"/>.</summary>
public class UserServiceTests : BankingTestBase
{
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(new AccountService(new TransactionService()));
    }

    [Fact]
    public void Register_PersistsTheUser()
    {
        var user = _userService.Register("Ada", "ada@example.com", "secret");

        var stored = UserRepository.Instance.FindByEmail("ada@example.com");
        Assert.NotNull(stored);
        Assert.Equal(user.Id, stored.Id);
        Assert.Equal("Ada", stored.Name);
    }

    [Fact]
    public void Register_CreatesAnActiveSavingsAccount()
    {
        var user = _userService.Register("Ada", "ada@example.com", "secret");

        var account = AccountRepository.Instance.GetAccountByUserId(user.Id);
        Assert.NotNull(account);
        Assert.Equal(AccountType.Savings, account.AccountType);
        Assert.Equal(AccountStatus.Active, account.AccountStatus);
        Assert.Equal(0.0, account.Balance);
    }

    [Fact]
    public void Register_ThrowsWhenEmailIsAlreadyTaken()
    {
        _userService.Register("Ada", "ada@example.com", "secret");

        var error = Assert.Throws<Exception>(
            () => _userService.Register("Someone Else", "ada@example.com", "other"));

        Assert.Contains("already exists", error.Message);
        Assert.Single(UserRepository.Instance.FindAll());
    }

    [Fact]
    public void Login_ReturnsTheUserForCorrectCredentials()
    {
        var registered = _userService.Register("Ada", "ada@example.com", "secret");

        var loggedIn = _userService.Login("ada@example.com", "secret");

        Assert.Equal(registered.Id, loggedIn.Id);
    }

    [Fact]
    public void Login_ThrowsForUnknownEmail()
    {
        var error = Assert.Throws<Exception>(() => _userService.Login("nobody@example.com", "secret"));

        Assert.Contains("No account found", error.Message);
    }

    [Fact]
    public void Login_ThrowsForWrongPassword()
    {
        _userService.Register("Ada", "ada@example.com", "secret");

        var error = Assert.Throws<Exception>(() => _userService.Login("ada@example.com", "wrong"));

        Assert.Equal("Incorrect password", error.Message);
    }
}
