using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

/// <summary>
/// Repository for persisting and retrieving <see cref="Transaction"/> entities to/from a JSON file.
/// Implements the singleton pattern.
/// </summary>
public class TransactionRepository : Repository<Transaction>
{
    private static readonly Lazy<TransactionRepository> _instance =
        new Lazy<TransactionRepository>(() => new TransactionRepository());

    /// <summary>Gets the singleton instance of <see cref="TransactionRepository"/>.</summary>
    public static TransactionRepository Instance => _instance.Value;

    private TransactionRepository() { }
    /// <summary>The underlying file name is "transactions.ndjson".</summary>
    protected override string FileName => "transactions.ndjson";

    /// <summary>Retrieves all transactions associated with the specified user.</summary>
    /// <param name="userId">The user's unique identifier.</param>
    /// <returns>A list of transactions, or <c>null</c> if the data file does not exist.</returns>
    public List<Transaction?> getTransactionsByUserId(string userId)
    {
        if (!File.Exists(FilePath)) return null;
        List<Transaction> transactions = new List<Transaction>();
        foreach (string line in File.ReadLines(FilePath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            Transaction? transaction = JsonSerializer.Deserialize<Transaction>(line);
            if(transaction.UserId == userId) transactions.Add(transaction);
        }

        return transactions;
    }
}