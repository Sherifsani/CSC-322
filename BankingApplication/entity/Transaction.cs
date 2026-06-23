namespace BankingApplication.entity;

public class Transaction : IEntity
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string AccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public double Amount { get; set; }

    public Transaction(){}

    public Transaction(string userId, string accountId, double amount, TransactionType type)
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = userId;
        this.AccountId = accountId;
        this.TransactionType = type;
        this.Amount = amount;
        
    }
}