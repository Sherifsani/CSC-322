using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;
    private readonly AccountService _accountService;

    public UserService(AccountService accountService)
    {
        _userRepository = UserRepository.Instance;
        _accountService = accountService;
    }

    public User Register(string name, string email, string password)
    {
        User existing = _userRepository.FindByEmail(email);
        if (existing != null)
        {
            throw new Exception("User already exists with email " + email);
        }

        User user = new User(name, email, password);
        _userRepository.Add(user);

        _accountService.CreateAccount(user.Id, AccountType.Savings);

        return user;
    }

    public User Login(string email, string password)
    {
        User user = _userRepository.FindByEmail(email);
        if (user == null)
        {
            throw new Exception("No account found with email " + email);
        }
        if (user.Password != password)
        {
            throw new Exception("Incorrect password");
        }
        return user;
    }
}
