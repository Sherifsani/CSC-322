using BankingApplication.entity;

namespace BankingApplication.service;

public interface IAccountService
{
    public Account CreateAccount(string userId, AccountType accountType);
    public Account GetAccountById(string id);
    public Account GetAccountByUserId(string userId);
    public void DeleteAccount(string id);
    public List<Account> GetAllAccounts();
}
