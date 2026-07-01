using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

/// <summary>
/// Repository for persisting and retrieving <see cref="User"/> entities to/from a JSON file.
/// Implements the singleton pattern.
/// </summary>
public class UserRepository : Repository<User>
{
    private static readonly Lazy<UserRepository> _instance = 
        new Lazy<UserRepository>(() => new UserRepository());

    /// <summary>Gets the singleton instance of <see cref="UserRepository"/>.</summary>
    public static UserRepository Instance => _instance.Value;

    private UserRepository() { }
    /// <summary>The underlying file name is "users.ndjson".</summary>
    protected override string FileName => "users.ndjson";

    /// <summary>Finds a user by their email address, or <c>null</c> if not found.</summary>
    /// <param name="email">The email to search for.</param>
    public User FindByEmail(string email)
    {
        if (!File.Exists(FilePath)) return null;
        foreach (string line in File.ReadLines(FilePath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue; 
            User? user = JsonSerializer.Deserialize<User>(line);
            if (user?.Email == email) return user;
        }

        return null;
    }
}