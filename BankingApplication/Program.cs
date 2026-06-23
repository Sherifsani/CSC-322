using BankingApplication.entity;
using BankingApplication.service.impl;

var userService = new UserService();
var accountService = new AccountService();
var transactionService = new TransactionService();

while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║         BANKING APPLICATION          ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("  [1] Manage Users");
    Console.WriteLine("  [2] Manage Accounts");
    Console.WriteLine("  [3] Manage Transactions");
    Console.WriteLine("  [0] Exit");
    Console.WriteLine();
    Console.Write("Select an option: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": ManageUsers(); break;
        case "2": ManageAccounts(); break;
        case "3": ManageTransactions(); break;
        case "0": return;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid option. Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
            break;
    }
}

void ManageUsers()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           MANAGE USERS               ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("  [1] Register User");
        Console.WriteLine("  [2] Find User by Email");
        Console.WriteLine("  [3] Find User by ID");
        Console.WriteLine("  [4] Delete User");
        Console.WriteLine("  [0] Back to Main Menu");
        Console.WriteLine();
        Console.Write("Select an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
            {
                Console.Write("Enter name: ");
                var name = Console.ReadLine();
                Console.Write("Enter email: ");
                var email = Console.ReadLine();
                Console.Write("Enter password: ");
                var password = Console.ReadLine();
                try
                {
                    var user = userService.registerUser(new User(name, email, password));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"User registered successfully! ID: {user.Id}");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {e.Message}");
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "2":
            {
                Console.Write("Enter email: ");
                var email = Console.ReadLine();
                var user = userService.GetUserByEmail(email);
                if (user == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("User not found.");
                }
                else
                {
                    PrintUser(user);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "3":
            {
                Console.Write("Enter user ID: ");
                var id = Console.ReadLine();
                var user = userService.GetUserById(id);
                if (user == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("User not found.");
                }
                else
                {
                    PrintUser(user);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "4":
            {
                Console.Write("Enter user ID to delete: ");
                var id = Console.ReadLine();
                userService.DeleteUser(id);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"User {id} deleted.");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "0": return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option.");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
        }
    }
}

void ManageAccounts()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║          MANAGE ACCOUNTS             ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("  [1] Create Account");
        Console.WriteLine("  [2] Find Account by ID");
        Console.WriteLine("  [3] Find Account by User ID");
        Console.WriteLine("  [4] Delete Account");
        Console.WriteLine("  [5] List All Accounts");
        Console.WriteLine("  [0] Back to Main Menu");
        Console.WriteLine();
        Console.Write("Select an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
            {
                Console.Write("Enter user ID: ");
                var userId = Console.ReadLine();
                Console.WriteLine("Account types:");
                foreach (var t in Enum.GetValues<AccountType>())
                    Console.WriteLine($"  {(int)t + 1} - {t}");
                Console.Write("Select account type: ");
                if (int.TryParse(Console.ReadLine(), out var typeIdx) &&
                    typeIdx >= 1 && typeIdx <= Enum.GetValues<AccountType>().Length)
                {
                    var accountType = (AccountType)(typeIdx - 1);
                    var account = accountService.CreateAccount(userId, accountType);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Account created! ID: {account.Id}, Type: {account.AccountType}, Balance: {account.Balance:C}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid account type.");
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "2":
            {
                Console.Write("Enter account ID: ");
                var id = Console.ReadLine();
                var account = accountService.GetAccountById(id);
                if (account == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Account not found.");
                }
                else
                {
                    PrintAccount(account);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "3":
            {
                Console.Write("Enter user ID: ");
                var userId = Console.ReadLine();
                var account = accountService.GetAccountByUserId(userId);
                if (account == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Account not found for this user.");
                }
                else
                {
                    PrintAccount(account);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "4":
            {
                Console.Write("Enter account ID to delete: ");
                var id = Console.ReadLine();
                accountService.DeleteAccount(id);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {id} deleted.");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "5":
            {
                var accounts = accountService.GetAllAccounts();
                if (accounts.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No accounts found.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nFound {accounts.Count} account(s):\n");
                    foreach (var a in accounts)
                        PrintAccount(a);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "0": return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option.");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
        }
    }
}

void ManageTransactions()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║        MANAGE TRANSACTIONS           ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("  [1] Create Transaction");
        Console.WriteLine("  [2] Find Transaction by ID");
        Console.WriteLine("  [3] Find Transactions by User ID");
        Console.WriteLine("  [4] List All Transactions");
        Console.WriteLine("  [0] Back to Main Menu");
        Console.WriteLine();
        Console.Write("Select an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
            {
                Console.Write("Enter user ID: ");
                var userId = Console.ReadLine();
                Console.Write("Enter account ID: ");
                var accountId = Console.ReadLine();
                Console.Write("Enter amount: ");
                if (!double.TryParse(Console.ReadLine(), out var amount))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid amount.");
                    Console.ResetColor();
                    break;
                }
                Console.WriteLine("Transaction types:");
                foreach (var t in Enum.GetValues<TransactionType>())
                    Console.WriteLine($"  {(int)t + 1} - {t}");
                Console.Write("Select transaction type: ");
                if (int.TryParse(Console.ReadLine(), out var typeIdx) &&
                    typeIdx >= 1 && typeIdx <= Enum.GetValues<TransactionType>().Length)
                {
                    var txType = (TransactionType)(typeIdx - 1);
                    var tx = transactionService.CreateTransaction(userId, accountId, amount, txType);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Transaction created! ID: {tx.Id}, Type: {tx.TransactionType}, Amount: {tx.Amount:C}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid transaction type.");
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "2":
            {
                Console.Write("Enter transaction ID: ");
                var id = Console.ReadLine();
                var tx = transactionService.GetTransactionById(id);
                if (tx == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Transaction not found.");
                }
                else
                {
                    PrintTransaction(tx);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "3":
            {
                Console.Write("Enter user ID: ");
                var userId = Console.ReadLine();
                var txs = transactionService.GetTransactionsByUserId(userId);
                if (txs == null || txs.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No transactions found for this user.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nFound {txs.Count} transaction(s):\n");
                    foreach (var t in txs)
                        PrintTransaction(t);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "4":
            {
                var txs = transactionService.GetAllTransactions();
                if (txs.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No transactions found.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nFound {txs.Count} transaction(s):\n");
                    foreach (var t in txs)
                        PrintTransaction(t);
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            case "0": return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option.");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
        }
    }
}

void PrintUser(User user)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n  ID:       {user.Id}");
    Console.WriteLine($"  Name:     {user.Name}");
    Console.WriteLine($"  Email:    {user.Email}");
    Console.WriteLine($"  Password: {user.Password}");
    Console.WriteLine($"  Account:  {user.AccountId}");
    Console.ResetColor();
}

void PrintAccount(Account account)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ID:     {account.Id}");
    Console.WriteLine($"  User:   {account.UserId}");
    Console.WriteLine($"  Type:   {account.AccountType}");
    Console.WriteLine($"  Status: {account.AccountStatus}");
    Console.WriteLine($"  Balance: {account.Balance:C}");
    Console.ResetColor();
}

void PrintTransaction(Transaction tx)
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"  ID:     {tx.Id}");
    Console.WriteLine($"  User:   {tx.UserId}");
    Console.WriteLine($"  Account: {tx.AccountId}");
    Console.WriteLine($"  Type:   {tx.TransactionType}");
    Console.WriteLine($"  Amount: {tx.Amount:C}");
    Console.ResetColor();
}
