using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

/// <summary>
/// Handles creation and retrieval of financial transaction records.
/// </summary>
public class TransactionService : ITransactionService
{
    private readonly TransactionRepository _transactionRepository;

    /// <summary>Initializes the service with the singleton repository instance.</summary>
    public TransactionService()
    {
        _transactionRepository = TransactionRepository.Instance;
    }

    /// <inheritdoc/>
    public Transaction CreateTransaction(string userId, string accountId, double amount, TransactionType type)
    {
        Transaction transaction = new Transaction(userId, accountId, amount, type);
        _transactionRepository.Add(transaction);
        return transaction;
    }

    /// <inheritdoc/>
    public Transaction GetTransactionById(string id)
    {
        return _transactionRepository.FindById(id);
    }

    /// <inheritdoc/>
    public List<Transaction?> GetTransactionsByUserId(string userId)
    {
        return _transactionRepository.getTransactionsByUserId(userId);
    }

    /// <inheritdoc/>
    public List<Transaction> GetAllTransactions()
    {
        return _transactionRepository.FindAll();
    }
}
