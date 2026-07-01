namespace BankingApplication.entity;

/// <summary>
/// Represents a bank account belonging to a user.
/// </summary>
public class Account : IEntity
{
    /// <summary>Gets or sets the unique account identifier.</summary>
    public string Id { get; set; }
    /// <summary>Gets or sets the ID of the user who owns this account.</summary>
    public string UserId { get; set; }
    /// <summary>Gets or sets the current balance.</summary>
    public double Balance { get; set; }
    /// <summary>Gets or sets the type of account (Savings, Fixed, Current).</summary>
    public AccountType AccountType { get; set; }
    /// <summary>Gets or sets the account status (Active, InActive, Closed).</summary>
    public AccountStatus AccountStatus { get; set; }

    /// <summary>Initializes a new empty <see cref="Account"/>.</summary>
    public Account(){}

    /// <summary>Initializes a new <see cref="Account"/> with a generated ID and zero balance.</summary>
    /// <param name="userId">The ID of the owning user.</param>
    /// <param name="accountType">The type of account to create.</param>
    public Account(string userId, AccountType accountType)
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = userId;
        this.Balance = 0.0;
        this.AccountType = accountType;
        this.AccountStatus = AccountStatus.Active;
    }
}