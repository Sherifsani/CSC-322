using BankingApplication.entity;

namespace BankingApplication.service;

/// <summary>
/// Defines operations for managing bank accounts.
/// </summary>
public interface IAccountService
{
    /// <summary>Retrieves the account belonging to the specified user.</summary>
    /// <param name="userId">The user's unique identifier.</param>
    /// <returns>The <see cref="Account"/>, or <c>null</c> if none exists.</returns>
    public Account GetAccountByUserId(string userId);
    
    /// <summary>Deposits money into an account and records a transaction.</summary>
    /// <param name="accountId">The target account ID.</param>
    /// <param name="amount">The amount to deposit.</param>
    /// <exception cref="Exception">Thrown if the account is not found or not active.</exception>
    public void Deposit(string accountId, double amount);
    
    /// <summary>Withdraws money from an account and records a transaction.</summary>
    /// <param name="accountId">The target account ID.</param>
    /// <param name="amount">The amount to withdraw.</param>
    /// <exception cref="Exception">Thrown if the account is not found, not active, or has insufficient balance.</exception>
    public void Withdraw(string accountId, double amount);
    
    /// <summary>Closes an account by marking its status as <see cref="AccountStatus.Closed"/>.</summary>
    /// <param name="accountId">The account ID to close.</param>
    /// <exception cref="Exception">Thrown if the account is not found or already closed.</exception>
    public void CloseAccount(string accountId);
}
