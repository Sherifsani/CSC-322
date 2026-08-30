using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.Tests;

/// <summary>Tests for the generic CRUD behaviour of <see cref="Repository{T}"/> and its subclasses.</summary>
public class RepositoryTests : BankingTestBase
{
    private readonly UserRepository _users = UserRepository.Instance;
    private readonly AccountRepository _accounts = AccountRepository.Instance;
    private readonly TransactionRepository _transactions = TransactionRepository.Instance;

    [Fact]
    public void Instance_IsSingleton()
    {
        Assert.Same(UserRepository.Instance, UserRepository.Instance);
        Assert.Same(AccountRepository.Instance, AccountRepository.Instance);
        Assert.Same(TransactionRepository.Instance, TransactionRepository.Instance);
    }

    [Fact]
    public void FindAll_ReturnsEmptyList_WhenFileDoesNotExist()
    {
        Assert.Empty(_users.FindAll());
    }

    [Fact]
    public void Add_AppendsOneLinePerEntity()
    {
        _users.Add(new User("A", "a@example.com", "p"));
        _users.Add(new User("B", "b@example.com", "p"));

        Assert.Equal(2, TestDatabase.ReadLines("users.ndjson").Length);
        Assert.Equal(2, _users.FindAll().Count);
    }

    [Fact]
    public void FindById_ReturnsMatchingEntity()
    {
        var user = new User("A", "a@example.com", "p");
        _users.Add(user);
        _users.Add(new User("B", "b@example.com", "p"));

        var found = _users.FindById(user.Id);

        Assert.NotNull(found);
        Assert.Equal("a@example.com", found.Email);
    }

    [Fact]
    public void FindById_ReturnsNull_WhenIdIsUnknown()
    {
        _users.Add(new User("A", "a@example.com", "p"));

        Assert.Null(_users.FindById("does-not-exist"));
    }

    [Fact]
    public void Update_ReplacesEntityWithSameId()
    {
        var user = new User("A", "a@example.com", "p");
        _users.Add(user);
        _users.Add(new User("B", "b@example.com", "p"));

        user.Name = "Updated";
        _users.Update(user);

        Assert.Equal("Updated", _users.FindById(user.Id).Name);
        Assert.Equal(2, _users.FindAll().Count);
    }

    [Fact]
    public void Update_DoesNothing_WhenIdIsUnknown()
    {
        _users.Add(new User("A", "a@example.com", "p"));

        _users.Update(new User("Ghost", "ghost@example.com", "p"));

        Assert.Single(_users.FindAll());
    }

    [Fact]
    public void Delete_RemovesOnlyTheMatchingEntity()
    {
        var user = new User("A", "a@example.com", "p");
        _users.Add(user);
        _users.Add(new User("B", "b@example.com", "p"));

        _users.delete(user.Id);

        var remaining = _users.FindAll();
        Assert.Single(remaining);
        Assert.Equal("b@example.com", remaining[0].Email);
    }

    [Fact]
    public void FindByEmail_ReturnsMatchingUser()
    {
        _users.Add(new User("A", "a@example.com", "p"));
        _users.Add(new User("B", "b@example.com", "p"));

        Assert.Equal("B", _users.FindByEmail("b@example.com").Name);
    }

    [Fact]
    public void FindByEmail_ReturnsNull_WhenEmailIsUnknown()
    {
        _users.Add(new User("A", "a@example.com", "p"));

        Assert.Null(_users.FindByEmail("nobody@example.com"));
    }

    [Fact]
    public void FindByEmail_ReturnsNull_WhenFileDoesNotExist()
    {
        Assert.Null(_users.FindByEmail("a@example.com"));
    }

    [Fact]
    public void GetAccountByUserId_ReturnsFirstAccountOfUser()
    {
        _accounts.Add(new Account("user-1", AccountType.Savings));
        var second = new Account("user-2", AccountType.Current);
        _accounts.Add(second);

        var found = _accounts.GetAccountByUserId("user-2");

        Assert.NotNull(found);
        Assert.Equal(second.Id, found.Id);
    }

    [Fact]
    public void GetAccountByUserId_ReturnsNull_WhenUserHasNoAccount()
    {
        _accounts.Add(new Account("user-1", AccountType.Savings));

        Assert.Null(_accounts.GetAccountByUserId("user-2"));
    }

    [Fact]
    public void GetTransactionsByUserId_ReturnsOnlyThatUsersTransactions()
    {
        _transactions.Add(new Transaction("user-1", "acc-1", 10, TransactionType.Deposit));
        _transactions.Add(new Transaction("user-2", "acc-2", 20, TransactionType.Deposit));
        _transactions.Add(new Transaction("user-1", "acc-1", 5, TransactionType.Withdrawal));

        var result = _transactions.getTransactionsByUserId("user-1");

        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.Equal("user-1", t!.UserId));
    }

    [Fact]
    public void GetTransactionsByUserId_ReturnsNull_WhenFileDoesNotExist()
    {
        // Documents current behaviour: the repository returns null rather than an empty list.
        Assert.Null(_transactions.getTransactionsByUserId("user-1"));
    }

    [Fact]
    public void Entities_SurviveASaveAndLoadRoundTrip()
    {
        var account = new Account("user-1", AccountType.Fixed) { Balance = 1234.56 };
        _accounts.Add(account);

        var loaded = _accounts.FindById(account.Id);

        Assert.Equal(account.UserId, loaded.UserId);
        Assert.Equal(account.Balance, loaded.Balance);
        Assert.Equal(AccountType.Fixed, loaded.AccountType);
        Assert.Equal(AccountStatus.Active, loaded.AccountStatus);
    }
}
