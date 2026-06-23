using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

public class AccountService : IAccountService
{
    private readonly AccountRepository _accountRepository;

    public AccountService()
    {
        _accountRepository = AccountRepository.Instance;
    }

    public Account CreateAccount(string userId, AccountType accountType)
    {
        Account account = new Account(userId, accountType);
        _accountRepository.Add(account);
        return account;
    }

    public Account GetAccountById(string id)
    {
        return _accountRepository.FindById(id);
    }

    public Account GetAccountByUserId(string userId)
    {
        return _accountRepository.GetAccountByUserId(userId);
    }

    public void DeleteAccount(string id)
    {
        _accountRepository.delete(id);
    }

    public List<Account> GetAllAccounts()
    {
        return _accountRepository.FindAll();
    }
}
