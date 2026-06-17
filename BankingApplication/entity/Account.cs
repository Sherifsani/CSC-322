namespace BankingApplication.entity;

public class Account : IEntity
{
    public string Id { get; }
    public string UserId { get; }
    public double Balance { get; set; }
    public AccountType AccountType { get; set; }
    public AccountStatus AccountStatus { get; set; }

    public Account(string userId, AccountType accountType)
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = userId;
        this.Balance = 0.0;
        this.AccountType = accountType;
        this.AccountStatus = AccountStatus.Active;
    }
}