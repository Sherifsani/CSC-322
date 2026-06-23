using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

public class TransactionRepository : Repository<Transaction>
{
    private static readonly Lazy<TransactionRepository> _instance =
        new Lazy<TransactionRepository>(() => new TransactionRepository());

    public static TransactionRepository Instance => _instance.Value;

    private TransactionRepository() { }
    protected override string FileName => "transactions.ndjson";

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