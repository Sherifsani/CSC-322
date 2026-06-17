using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

public class UserRepository : Repository<User>
{
    protected override string FilePath => "/db/users.ndjson";

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