namespace BankingApplication.entity;

/// <summary>
/// Represents a bank institution that holds users and accounts.
/// </summary>
public class Bank
{
    private string Name { get; set; }
    private string Address { get; set; }
    private List<User> Users { get; set; }
    /// <summary>Initializes a new empty <see cref="Bank"/>.</summary>
    public Bank() {}
    
}