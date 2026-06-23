using BankingApplication.entity;

namespace BankingApplication.service;

public interface IAccountService
{
    public Account GetAccountByUserId(string userId);
    public void Deposit(string accountId, double amount);
    public void Withdraw(string accountId, double amount);
    public void CloseAccount(string accountId);
}
