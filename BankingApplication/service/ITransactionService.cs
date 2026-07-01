using BankingApplication.entity;

namespace BankingApplication.service;

/// <summary>
/// Defines operations for recording and querying financial transactions.
/// </summary>
public interface ITransactionService
{
    /// <summary>Creates and persists a new transaction record.</summary>
    /// <param name="userId">The ID of the user performing the transaction.</param>
    /// <param name="accountId">The ID of the account involved.</param>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="type">Whether this is a deposit or withdrawal.</param>
    /// <returns>The newly created <see cref="Transaction"/>.</returns>
    public Transaction CreateTransaction(string userId, string accountId, double amount, TransactionType type);
    
    /// <summary>Retrieves a transaction by its unique identifier.</summary>
    /// <param name="id">The transaction ID.</param>
    /// <returns>The matching <see cref="Transaction"/>, or <c>null</c>.</returns>
    public Transaction GetTransactionById(string id);
    
    /// <summary>Retrieves all transactions for a given user.</summary>
    /// <param name="userId">The user's unique identifier.</param>
    /// <returns>A list of transactions, or <c>null</c>.</returns>
    public List<Transaction?> GetTransactionsByUserId(string userId);
    
    /// <summary>Retrieves all transactions across all users.</summary>
    /// <returns>A list of all transactions.</returns>
    public List<Transaction> GetAllTransactions();
}
