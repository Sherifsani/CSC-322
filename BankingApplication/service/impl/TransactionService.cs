using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

public class TransactionService : ITransactionService
{
    private readonly TransactionRepository _transactionRepository;

    public TransactionService()
    {
        _transactionRepository = TransactionRepository.Instance;
    }

    public Transaction CreateTransaction(string userId, string accountId, double amount, TransactionType type)
    {
        Transaction transaction = new Transaction(userId, accountId, amount, type);
        _transactionRepository.Add(transaction);
        return transaction;
    }

    public Transaction GetTransactionById(string id)
    {
        return _transactionRepository.FindById(id);
    }

    public List<Transaction?> GetTransactionsByUserId(string userId)
    {
        return _transactionRepository.getTransactionsByUserId(userId);
    }

    public List<Transaction> GetAllTransactions()
    {
        return _transactionRepository.FindAll();
    }
}
