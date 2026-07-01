namespace BankingApplication.entity;

/// <summary>
/// Represents a financial transaction (deposit or withdrawal) on an account.
/// </summary>
public class Transaction : IEntity
{
    /// <summary>Gets or sets the unique transaction identifier.</summary>
    public string Id { get; set; }
    /// <summary>Gets or sets the ID of the user who performed the transaction.</summary>
    public string UserId { get; set; }
    /// <summary>Gets or sets the ID of the account involved.</summary>
    public string AccountId { get; set; }
    /// <summary>Gets or sets the type of transaction (Deposit or Withdrawal).</summary>
    public TransactionType TransactionType { get; set; }
    /// <summary>Gets or sets the monetary amount of the transaction.</summary>
    public double Amount { get; set; }

    /// <summary>Initializes a new empty <see cref="Transaction"/>.</summary>
    public Transaction(){}

    /// <summary>Initializes a new <see cref="Transaction"/> with a generated ID.</summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="accountId">The ID of the account.</param>
    /// <param name="amount">The transaction amount.</param>
    /// <param name="type">Whether this is a deposit or withdrawal.</param>
    public Transaction(string userId, string accountId, double amount, TransactionType type)
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = userId;
        this.AccountId = accountId;
        this.TransactionType = type;
        this.Amount = amount;
        
    }
}