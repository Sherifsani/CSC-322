using BankingApplication.entity;

namespace BankingApplication.service;

/// <summary>
/// Defines operations for user registration and authentication.
/// </summary>
public interface IUserService
{
    /// <summary>Registers a new user and creates a savings account.</summary>
    /// <param name="name">The user's full name.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>The newly created <see cref="User"/>.</returns>
    /// <exception cref="Exception">Thrown if the email is already registered.</exception>
    public User Register(string name, string email, string password);
    
    /// <summary>Authenticates a user by email and password.</summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>The authenticated <see cref="User"/>.</returns>
    /// <exception cref="Exception">Thrown if credentials are invalid.</exception>
    public User Login(string email, string password);
}
