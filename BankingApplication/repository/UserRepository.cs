using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

public class UserRepository : Repository<User>
{
    // Thread-safe, lazy-initialized instance holder
    private static readonly Lazy<UserRepository> _instance = 
        new Lazy<UserRepository>(() => new UserRepository());

    // Public entry point to access the one and only instance
    public static UserRepository Instance => _instance.Value;

    // Private constructor completely stops external 'new UserRepository()'
    private UserRepository() { }
    protected override string FileName => "users.ndjson";

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