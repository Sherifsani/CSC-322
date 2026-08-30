using BankingApplication.entity;
using BankingApplication.service.impl;

namespace BankingApplication.Tests;

/// <summary>
/// End-to-end tests that exercise the services together, the way the console application uses them.
/// </summary>
public class BankingFlowTests : BankingTestBase
{
    private readonly TransactionService _transactionService = new();
    private readonly AccountService _accountService;
    private readonly UserService _userService;

    public BankingFlowTests()
    {
        _accountService = new AccountService(_transactionService);
        _userService = new UserService(_accountService);
    }

    [Fact]
    public void SignUpDepositWithdrawAndReviewHistory()
    {
        var user = _userService.Register("Ada", "ada@example.com", "secret");
        var account = _accountService.GetAccountByUserId(user.Id);

        _accountService.Deposit(account.Id, 500);
        _accountService.Withdraw(account.Id, 120.50);

        Assert.Equal(379.50, _accountService.GetAccountByUserId(user.Id).Balance);

        var history = _transactionService.GetTransactionsByUserId(user.Id);
        Assert.Equal(2, history.Count);
        Assert.Equal(TransactionType.Deposit, history[0]!.TransactionType);
        Assert.Equal(TransactionType.Withdrawal, history[1]!.TransactionType);
    }

    [Fact]
    public void ClosedAccountCanNoLongerTransact()
    {
        var user = _userService.Register("Ada", "ada@example.com", "secret");
        var account = _accountService.GetAccountByUserId(user.Id);
        _accountService.Deposit(account.Id, 100);

        _accountService.CloseAccount(account.Id);

        Assert.Throws<Exception>(() => _accountService.Deposit(account.Id, 10));
        Assert.Throws<Exception>(() => _accountService.Withdraw(account.Id, 10));
        Assert.Single(_transactionService.GetTransactionsByUserId(user.Id));
        Assert.Equal(100, _accountService.GetAccountByUserId(user.Id).Balance);
    }

    [Fact]
    public void EachUsersHistoryAndBalanceStayIndependent()
    {
        var ada = _userService.Register("Ada", "ada@example.com", "secret");
        var alan = _userService.Register("Alan", "alan@example.com", "secret");

        _accountService.Deposit(_accountService.GetAccountByUserId(ada.Id).Id, 300);
        _accountService.Deposit(_accountService.GetAccountByUserId(alan.Id).Id, 75);

        Assert.Equal(300, _accountService.GetAccountByUserId(ada.Id).Balance);
        Assert.Equal(75, _accountService.GetAccountByUserId(alan.Id).Balance);
        Assert.Single(_transactionService.GetTransactionsByUserId(ada.Id));
        Assert.Single(_transactionService.GetTransactionsByUserId(alan.Id));
        Assert.Equal(2, _transactionService.GetAllTransactions().Count);
    }

    [Fact]
    public void LoginAfterRegistrationReturnsTheSameAccount()
    {
        var registered = _userService.Register("Ada", "ada@example.com", "secret");
        var accountId = _accountService.GetAccountByUserId(registered.Id).Id;

        var loggedIn = _userService.Login("ada@example.com", "secret");

        Assert.Equal(accountId, _accountService.GetAccountByUserId(loggedIn.Id).Id);
    }
}
