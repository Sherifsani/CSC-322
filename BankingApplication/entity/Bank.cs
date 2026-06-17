namespace BankingApplication.entity;

public class Bank
{
    private string Name { get; set; }
    private string Address { get; set; }
    private List<User> Users { get; set; }
    public Bank() {}
    
}