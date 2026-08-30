using BankingApplication.entity;
using BankingApplication.service.impl;

namespace BankingApplication.Tests;

/// <summary>Tests for <see cref="TransactionService"/>.</summary>
public class TransactionServiceTests : BankingTestBase
{
    private readonly TransactionService _service = new();

    [Fact]
    public void CreateTransaction_ReturnsAPersistedTransaction()
    {
        var transaction = _service.CreateTransaction("user-1", "acc-1", 42.5, TransactionType.Deposit);

        Assert.Equal("user-1", transaction.UserId);
        Assert.Equal("acc-1", transaction.AccountId);
        Assert.Equal(42.5, transaction.Amount);
        Assert.Equal(TransactionType.Deposit, transaction.TransactionType);
        Assert.Equal(transaction.Id, _service.GetTransactionById(transaction.Id).Id);
    }

    [Fact]
    public void GetTransactionById_ReturnsNull_WhenIdIsUnknown()
    {
        _service.CreateTransaction("user-1", "acc-1", 10, TransactionType.Deposit);

        Assert.Null(_service.GetTransactionById("does-not-exist"));
    }

    [Fact]
    public void GetTransactionsByUserId_FiltersByUser()
    {
        _service.CreateTransaction("user-1", "acc-1", 10, TransactionType.Deposit);
        _service.CreateTransaction("user-2", "acc-2", 20, TransactionType.Deposit);
        _service.CreateTransaction("user-1", "acc-1", 5, TransactionType.Withdrawal);

        var result = _service.GetTransactionsByUserId("user-1");

        Assert.Equal(2, result.Count);
        Assert.Equal(new[] { 10.0, 5.0 }, result.Select(t => t!.Amount));
    }

    [Fact]
    public void GetTransactionsByUserId_ReturnsEmpty_WhenUserHasNoTransactions()
    {
        _service.CreateTransaction("user-1", "acc-1", 10, TransactionType.Deposit);

        Assert.Empty(_service.GetTransactionsByUserId("user-2"));
    }

    [Fact]
    public void GetAllTransactions_ReturnsEveryTransactionInInsertionOrder()
    {
        _service.CreateTransaction("user-1", "acc-1", 10, TransactionType.Deposit);
        _service.CreateTransaction("user-2", "acc-2", 20, TransactionType.Withdrawal);

        var all = _service.GetAllTransactions();

        Assert.Equal(2, all.Count);
        Assert.Equal(new[] { 10.0, 20.0 }, all.Select(t => t.Amount));
    }

    [Fact]
    public void GetAllTransactions_ReturnsEmpty_WhenNothingHasBeenRecorded()
    {
        Assert.Empty(_service.GetAllTransactions());
    }
}
