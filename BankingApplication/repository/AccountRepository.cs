using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

public class AccountRepository : Repository<Account>
{
    protected override string FilePath => "/db/accounts.ndjson";

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