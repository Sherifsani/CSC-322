using BankingApplication.entity;
using BankingApplication.repository;

namespace BankingApplication.service.impl;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService()
    {
        _userRepository = UserRepository.Instance;
    }

    public User registerUser(User user)
    {
        User existingUser = _userRepository.FindByEmail(user.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists with email " + user.Email);
        }
        _userRepository.Add(user);
        return user;
    }

    public User GetUserByEmail(string email)
    {
        return _userRepository.FindByEmail(email);
    }

    public User GetUserById(string id)
    {
        return _userRepository.FindById(id);
    }

    // public void UpdateUser(User user)
    // {
    //     User existingUser = _userRepository.FindByEmail(user.Email);
    //     if (existingUser != null)
    //     {
    //         
    //     }
    // }

    public void DeleteUser(string id)
    {
        _userRepository.delete(id);
    }
}
