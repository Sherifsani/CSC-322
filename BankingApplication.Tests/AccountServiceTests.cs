using BankingApplication.entity;
using BankingApplication.repository;
using BankingApplication.service.impl;

namespace BankingApplication.Tests;

/// <summary>Tests for deposits, withdrawals and account lifecycle in <see cref="AccountService"/>.</summary>
public class AccountServiceTests : BankingTestBase
{
    private readonly TransactionService _transactionService = new();
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _accountService = new AccountService(_transactionService);
    }

    private Account NewAccount(double startingBalance = 0.0, AccountStatus status = AccountStatus.Active)
    {
        var account = _accountService.CreateAccount("user-1", AccountType.Savings);
        if (startingBalance != 0.0 || status != AccountStatus.Active)
        {
            account.Balance = startingBalance;
            account.AccountStatus = status;
            AccountRepository.Instance.Update(account);
        }
        return account;
    }

    private static Account Reload(Account account) => AccountRepository.Instance.FindById(account.Id);

    [Fact]
    public void CreateAccount_PersistsAnActiveAccountWithZeroBalance()
    {
        var account = _accountService.CreateAccount("user-1", AccountType.Current);

        var stored = Reload(account);
        Assert.NotNull(stored);
        Assert.Equal("user-1", stored.UserId);
        Assert.Equal(0.0, stored.Balance);
        Assert.Equal(AccountType.Current, stored.AccountType);
        Assert.Equal(AccountStatus.Active, stored.AccountStatus);
    }

    [Fact]
    public void GetAccountByUserId_ReturnsTheUsersAccount()
    {
        var account = _accountService.CreateAccount("user-1", AccountType.Savings);
        _accountService.CreateAccount("user-2", AccountType.Savings);

        Assert.Equal(account.Id, _accountService.GetAccountByUserId("user-1").Id);
    }

    [Fact]
    public void GetAccountByUserId_ReturnsNull_WhenTheUserHasNoAccount()
    {
        Assert.Null(_accountService.GetAccountByUserId("user-1"));
    }

    [Fact]
    public void Deposit_IncreasesTheStoredBalance()
    {
        var account = NewAccount();

        _accountService.Deposit(account.Id, 100);
        _accountService.Deposit(account.Id, 50.25);

        Assert.Equal(150.25, Reload(account).Balance);
    }

    [Fact]
    public void Deposit_RecordsADepositTransaction()
    {
        var account = NewAccount();

        _accountService.Deposit(account.Id, 100);

        var transaction = Assert.Single(_transactionService.GetAllTransactions());
        Assert.Equal(account.Id, transaction.AccountId);
        Assert.Equal("user-1", transaction.UserId);
        Assert.Equal(100, transaction.Amount);
        Assert.Equal(TransactionType.Deposit, transaction.TransactionType);
    }

    [Fact]
    public void Deposit_ThrowsWhenAccountDoesNotExist()
    {
        var error = Assert.Throws<Exception>(() => _accountService.Deposit("missing", 100));

        Assert.Equal("Account not found", error.Message);
    }

    [Theory]
    [InlineData(AccountStatus.InActive)]
    [InlineData(AccountStatus.Closed)]
    public void Deposit_ThrowsWhenAccountIsNotActive(AccountStatus status)
    {
        var account = NewAccount(status: status);

        var error = Assert.Throws<Exception>(() => _accountService.Deposit(account.Id, 100));

        Assert.Equal("Account is not active", error.Message);
        Assert.Empty(_transactionService.GetAllTransactions());
    }

    [Fact]
    public void Withdraw_DecreasesTheStoredBalance()
    {
        var account = NewAccount(startingBalance: 200);

        _accountService.Withdraw(account.Id, 75);

        Assert.Equal(125, Reload(account).Balance);
    }

    [Fact]
    public void Withdraw_AllowsEmptyingTheAccount()
    {
        var account = NewAccount(startingBalance: 200);

        _accountService.Withdraw(account.Id, 200);

        Assert.Equal(0, Reload(account).Balance);
    }

    [Fact]
    public void Withdraw_RecordsAWithdrawalTransaction()
    {
        var account = NewAccount(startingBalance: 200);

        _accountService.Withdraw(account.Id, 75);

        var transaction = Assert.Single(_transactionService.GetAllTransactions());
        Assert.Equal(75, transaction.Amount);
        Assert.Equal(TransactionType.Withdrawal, transaction.TransactionType);
    }

    [Fact]
    public void Withdraw_ThrowsWhenAccountDoesNotExist()
    {
        var error = Assert.Throws<Exception>(() => _accountService.Withdraw("missing", 10));

        Assert.Equal("Account not found", error.Message);
    }

    [Fact]
    public void Withdraw_ThrowsWhenAccountIsNotActive()
    {
        var account = NewAccount(startingBalance: 200, status: AccountStatus.Closed);

        var error = Assert.Throws<Exception>(() => _accountService.Withdraw(account.Id, 10));

        Assert.Equal("Account is not active", error.Message);
    }

    [Fact]
    public void Withdraw_ThrowsWhenBalanceIsInsufficient()
    {
        var account = NewAccount(startingBalance: 50);

        var error = Assert.Throws<Exception>(() => _accountService.Withdraw(account.Id, 50.01));

        Assert.Equal("Insufficient balance", error.Message);
        Assert.Equal(50, Reload(account).Balance);
        Assert.Empty(_transactionService.GetAllTransactions());
    }

    [Fact]
    public void CloseAccount_MarksTheAccountClosed()
    {
        var account = NewAccount();

        _accountService.CloseAccount(account.Id);

        Assert.Equal(AccountStatus.Closed, Reload(account).AccountStatus);
    }

    [Fact]
    public void CloseAccount_ThrowsWhenAccountIsAlreadyClosed()
    {
        var account = NewAccount();
        _accountService.CloseAccount(account.Id);

        var error = Assert.Throws<Exception>(() => _accountService.CloseAccount(account.Id));

        Assert.Equal("Account is already closed", error.Message);
    }

    [Fact]
    public void CloseAccount_ThrowsWhenAccountDoesNotExist()
    {
        var error = Assert.Throws<Exception>(() => _accountService.CloseAccount("missing"));

        Assert.Equal("Account not found", error.Message);
    }
}
