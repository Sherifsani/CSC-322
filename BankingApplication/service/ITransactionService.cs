using BankingApplication.entity;

namespace BankingApplication.service;

public interface ITransactionService
{
    public Transaction CreateTransaction(string userId, string accountId, double amount, TransactionType type);
    public Transaction GetTransactionById(string id);
    public List<Transaction?> GetTransactionsByUserId(string userId);
    public List<Transaction> GetAllTransactions();
}
