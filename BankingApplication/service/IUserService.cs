using BankingApplication.entity;

namespace BankingApplication.service;

public interface IUserService
{
    public User registerUser(User user);
    public User GetUserByEmail(string email);
    public User GetUserById(string id);
    // public void  UpdateUser(User user);
    public void DeleteUser(string id);
}