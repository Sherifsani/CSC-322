using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

/// <summary>
/// Manages bank account operations including creation, deposits, withdrawals, and closing.
/// Records transactions via <see cref="TransactionService"/>.
/// </summary>
public class AccountService : IAccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly TransactionService _transactionService;

    /// <summary>Initializes the service with singleton repository instances.</summary>
    /// <param name="transactionService">The transaction service for recording deposits and withdrawals.</param>
    public AccountService(TransactionService transactionService)
    {
        _accountRepository = AccountRepository.Instance;
        _transactionService = transactionService;
    }

    /// <summary>Creates a new account with zero balance and Active status.</summary>
    /// <param name="userId">The ID of the owning user.</param>
    /// <param name="accountType">The type of account (Savings, Fixed, Current).</param>
    /// <returns>The newly created <see cref="Account"/>.</returns>
    public Account CreateAccount(string userId, AccountType accountType)
    {
        Account account = new Account(userId, accountType);
        _accountRepository.Add(account);
        return account;
    }

    /// <inheritdoc/>
    public Account GetAccountByUserId(string userId)
    {
        return _accountRepository.GetAccountByUserId(userId);
    }

    /// <inheritdoc/>
    public void Deposit(string accountId, double amount)
    {
        Account account = _accountRepository.FindById(accountId);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        if (account.AccountStatus != AccountStatus.Active)
        {
            throw new Exception("Account is not active");
        }

        account.Balance += amount;
        _accountRepository.Update(account);

        _transactionService.CreateTransaction(account.UserId, accountId, amount, TransactionType.Deposit);
    }

    /// <inheritdoc/>
    public void Withdraw(string accountId, double amount)
    {
        Account account = _accountRepository.FindById(accountId);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        if (account.AccountStatus != AccountStatus.Active)
        {
            throw new Exception("Account is not active");
        }
        if (account.Balance < amount)
        {
            throw new Exception("Insufficient balance");
        }

        account.Balance -= amount;
        _accountRepository.Update(account);

        _transactionService.CreateTransaction(account.UserId, accountId, amount, TransactionType.Withdrawal);
    }

    /// <inheritdoc/>
    public void CloseAccount(string accountId)
    {
        Account account = _accountRepository.FindById(accountId);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        if (account.AccountStatus == AccountStatus.Closed)
        {
            throw new Exception("Account is already closed");
        }

        account.AccountStatus = AccountStatus.Closed;
        _accountRepository.Update(account);
    }
}
