using BankingApplication.entity;

namespace BankingApplication.service;

public interface IUserService
{
    public User Register(string name, string email, string password);
    public User Login(string email, string password);
}
