using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

/// <summary>
/// Handles user registration and authentication, delegating persistence to <see cref="UserRepository"/>.
/// </summary>
public class UserService : IUserService
{
    private readonly UserRepository _userRepository;
    private readonly AccountService _accountService;

    /// <summary>Initializes the service with the singleton repository instances.</summary>
    /// <param name="accountService">The account service used to create a savings account on registration.</param>
    public UserService(AccountService accountService)
    {
        _userRepository = UserRepository.Instance;
        _accountService = accountService;
    }

    /// <summary>Registers a new user and automatically creates a Savings account.</summary>
    /// <inheritdoc/>
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

    /// <summary>Authenticates a user by email and password.</summary>
    /// <inheritdoc/>
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
