namespace BankingApplication.entity;

public class User : IEntity
{
    public string Id { get;  }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AccountId { get; }
    
    // public List<Transaction> Transactions { get; set; }
    
    public User(){}

    public User(string name, string email, string password)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Id = Guid.NewGuid().ToString();
        this.AccountId = Guid.NewGuid().ToString();
    }
}