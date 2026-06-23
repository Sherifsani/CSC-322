using BankingApplication.entity;
using BankingApplication.service.impl;

var transactionService = new TransactionService();
var accountService = new AccountService(transactionService);
var userService = new UserService(accountService);

while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║         BANKING APPLICATION          ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("  [1] Login");
    Console.WriteLine("  [2] Sign Up");
    Console.WriteLine("  [0] Exit");
    Console.WriteLine();
    Console.Write("Select an option: ");
    var choice = Console.ReadLine();

    User currentUser = null;

    switch (choice)
    {
        case "1":
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();
            try
            {
                currentUser = userService.Login(email, password);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                continue;
            }
            break;
        }
        case "2":
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();
            try
            {
                currentUser = userService.Register(name, email, password);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Account created successfully!");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                continue;
            }
            break;
        }
        case "0": return;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid option.");
            Console.ResetColor();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
    }

    // --- Logged in ---
    while (currentUser != null)
    {
        Account account = accountService.GetAccountByUserId(currentUser.Id);

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║              DASHBOARD               ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"  Welcome, {currentUser.Name}!");
        Console.WriteLine();

        if (account != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  Account:     {account.Id}");
            Console.WriteLine($"  Type:        {account.AccountType}");
            Console.WriteLine($"  Status:      {account.AccountStatus}");
            Console.WriteLine($"  Balance:     {account.Balance:C}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  No account found.");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine("  [1] Deposit");
        Console.WriteLine("  [2] Withdraw");
        Console.WriteLine("  [3] Transaction History");
        Console.WriteLine("  [4] Close Account");
        Console.WriteLine("  [0] Logout");
        Console.WriteLine();
        Console.Write("Select an option: ");
        var option = Console.ReadLine();

        try
        {
            switch (option)
            {
                case "1":
                {
                    if (account == null) throw new Exception("No account found.");
                    Console.Write("Deposit amount: ");
                    if (!double.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
                    {
                        throw new Exception("Invalid amount.");
                    }
                    accountService.Deposit(account.Id, amount);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Successfully deposited {amount:C}.");
                    Console.ResetColor();
                    break;
                }
                case "2":
                {
                    if (account == null) throw new Exception("No account found.");
                    Console.Write("Withdrawal amount: ");
                    if (!double.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
                    {
                        throw new Exception("Invalid amount.");
                    }
                    accountService.Withdraw(account.Id, amount);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Successfully withdrew {amount:C}.");
                    Console.ResetColor();
                    break;
                }
                case "3":
                {
                    var txs = transactionService.GetTransactionsByUserId(currentUser.Id);
                    if (txs == null || txs.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No transactions found.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"\nTransaction History ({txs.Count}):\n");
                        foreach (var t in txs)
                        {
                            Console.WriteLine($"  {t.TransactionType,-10} {t.Amount,10:C}  ({t.Id})");
                        }
                    }
                    Console.ResetColor();
                    break;
                }
                case "4":
                {
                    if (account == null) throw new Exception("No account found.");
                    accountService.CloseAccount(account.Id);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Account has been closed.");
                    Console.ResetColor();
                    break;
                }
                case "0":
                {
                    currentUser = null;
                    continue;
                }
                default:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option.");
                    Console.ResetColor();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
