namespace BankingApplication.entity;

/// <summary>
/// Represents a bank customer with login credentials.
/// </summary>
public class User : IEntity
{
    /// <summary>Gets or sets the unique identifier.</summary>
    public string Id { get; set; }
    /// <summary>Gets or sets the full name of the user.</summary>
    public string Name { get; set; }
    /// <summary>Gets or sets the email address used for login.</summary>
    public string Email { get; set; }
    /// <summary>Gets or sets the password for authentication.</summary>
    public string Password { get; set; }

    /// <summary>Initializes a new empty <see cref="User"/>.</summary>
    public User(){}

    /// <summary>Initializes a new <see cref="User"/> with a generated ID.</summary>
    /// <param name="name">The user's full name.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    public User(string name, string email, string password)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Id = Guid.NewGuid().ToString();
    }
}