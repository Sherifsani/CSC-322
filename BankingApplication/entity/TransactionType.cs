namespace BankingApplication.entity;

/// <summary>Specifies the direction of a financial transaction.</summary>
public enum TransactionType
{
    /// <summary>Money was removed from the account.</summary>
    Withdrawal, 
    /// <summary>Money was added to the account.</summary>
    Deposit
}