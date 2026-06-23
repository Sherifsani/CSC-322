using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

public class AccountService : IAccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly TransactionService _transactionService;

    public AccountService(TransactionService transactionService)
    {
        _accountRepository = AccountRepository.Instance;
        _transactionService = transactionService;
    }

    public Account CreateAccount(string userId, AccountType accountType)
    {
        Account account = new Account(userId, accountType);
        _accountRepository.Add(account);
        return account;
    }

    public Account GetAccountByUserId(string userId)
    {
        return _accountRepository.GetAccountByUserId(userId);
    }

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
