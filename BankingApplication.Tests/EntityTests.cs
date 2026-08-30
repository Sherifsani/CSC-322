using BankingApplication.entity;

namespace BankingApplication.Tests;

/// <summary>Tests for the entity constructors and their default values.</summary>
public class EntityTests
{
    [Fact]
    public void UserConstructor_SetsFieldsAndGeneratesId()
    {
        var user = new User("Ada Lovelace", "ada@example.com", "secret");

        Assert.Equal("Ada Lovelace", user.Name);
        Assert.Equal("ada@example.com", user.Email);
        Assert.Equal("secret", user.Password);
        Assert.True(Guid.TryParse(user.Id, out _));
    }

    [Fact]
    public void UserConstructor_GeneratesUniqueIds()
    {
        var first = new User("A", "a@example.com", "p");
        var second = new User("B", "b@example.com", "p");

        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void AccountConstructor_StartsActiveWithZeroBalance()
    {
        var account = new Account("user-1", AccountType.Savings);

        Assert.Equal("user-1", account.UserId);
        Assert.Equal(0.0, account.Balance);
        Assert.Equal(AccountType.Savings, account.AccountType);
        Assert.Equal(AccountStatus.Active, account.AccountStatus);
        Assert.True(Guid.TryParse(account.Id, out _));
    }

    [Fact]
    public void TransactionConstructor_SetsFieldsAndGeneratesId()
    {
        var transaction = new Transaction("user-1", "account-1", 250.5, TransactionType.Deposit);

        Assert.Equal("user-1", transaction.UserId);
        Assert.Equal("account-1", transaction.AccountId);
        Assert.Equal(250.5, transaction.Amount);
        Assert.Equal(TransactionType.Deposit, transaction.TransactionType);
        Assert.True(Guid.TryParse(transaction.Id, out _));
    }
}
