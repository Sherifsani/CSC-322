using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

/// <summary>
/// Repository for persisting and retrieving <see cref="Account"/> entities to/from a JSON file.
/// Implements the singleton pattern.
/// </summary>
public class AccountRepository : Repository<Account>
{
    private static readonly Lazy<AccountRepository> _instance =
        new Lazy<AccountRepository>(() => new AccountRepository());

    /// <summary>Gets the singleton instance of <see cref="AccountRepository"/>.</summary>
    public static AccountRepository Instance => _instance.Value;

    private AccountRepository() { }
    /// <summary>The underlying file name is "accounts.ndjson".</summary>
    protected override string FileName => "accounts.ndjson";

    /// <summary>Retrieves the first account belonging to the specified user.</summary>
    /// <param name="userId">The user's unique identifier.</param>
    /// <returns>The matching <see cref="Account"/>, or <c>null</c> if none exists.</returns>
      public Account? GetAccountByUserId(string userId)                                                                                                                                                      
      {                                                                                                                                                                                                      
          if (!File.Exists(FilePath)) return null;                                                                                                                                                           
          foreach (string line in File.ReadLines(FilePath))                                                                                                                                                  
          {                                                                                                                                                                                                  
              if (string.IsNullOrWhiteSpace(line)) continue;                                                                                                                                                 
              var account = JsonSerializer.Deserialize<Account>(line);                                                                                                                                       
              if (account?.UserId == userId) return account;                                                                                                                                                 
          }                                                                                                                                                                                                  
          return null;                                                                                                                                                                                       
      }  
}