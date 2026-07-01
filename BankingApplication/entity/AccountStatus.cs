namespace BankingApplication.entity;

/// <summary>Specifies the operational status of an account.</summary>
public enum AccountStatus
{
    /// <summary>The account is active and can perform transactions.</summary>
    Active, 
    /// <summary>The account is inactive and cannot perform transactions.</summary>
    InActive, 
    /// <summary>The account is closed and cannot be used.</summary>
    Closed
}