using TransactionSystem.DomainLogic.AccountsService;
using TransactionSystem.DomainLogic.Transaction;
using TransactionSystem.DomainLogic.UserAdministration;
using TransactionSystem.Entityes;

class Program
{
    static void Main()
    {
        IUserAdministrationRepo userAdministration = new UserAdministrationService();
        IAccountRepo accountService = new AccountService(userAdministration);
        ITransactionRepo svc = new TransactionService(accountService);
        var user = new User("Adrian Krastev");

        while (true)
        {
            Console.WriteLine("\n0) Create User  1) Create  2) Deposit  3) Withdraw  4) Balance  5) Transfer  6) Exit");

            var opreartion = Console.ReadLine();

            try
            {
                switch (opreartion)
                {
                    case "0"://Create User
                        Console.Write("User Name: ");
                        var name = Console.ReadLine();
                        user = new User(name);
                        Console.WriteLine($"User {name} created.");
                        break;

                    case "1"://Create Account
                        Console.Write("Set Name: ");
                        var accountname = Console.ReadLine();
                        Console.Write("AccountId #: ");
                        var no = Console.ReadLine();
                        Console.Write("Set Balance: ");
                        Console.WriteLine("Balance is required!!!");
                        decimal init = decimal.Parse(Console.ReadLine());
                        accountService.CreateAccountAsync(user,accountname, no, init);
                        Console.WriteLine("Account created.");
                        break;
                    case "2"://Deposit
                        Console.Write("Acct #: ");
                        no = Console.ReadLine();
                        Console.Write("Amount: ");
                        if(no == null)
                            throw new ArgumentNullException("Account number cannot be null.");
                        svc.DepositAsync(no, decimal.Parse(Console.ReadLine()));
                        Console.WriteLine($"New Balance: {accountService.GetBalanceAsync(no)}");
                        break;
                    case "3"://Withdraw
                        Console.Write("Acct #: ");
                        no = Console.ReadLine();
                        Console.Write("Amount: ");
                        svc.WithdrawAsync(no, decimal.Parse(Console.ReadLine()));
                        Console.WriteLine($"New Balance: {accountService.GetBalanceAsync(no)}");
                        break;
                    case "4"://Balance
                        Console.Write("Acct #: ");
                        no = Console.ReadLine();
                        Console.WriteLine($"Balance: {accountService.GetBalanceAsync(no)}");
                        break;
                    case "5"://Transfer
                        Console.Write("From Acct #: ");
                        var from = Console.ReadLine();
                        Console.Write("To Acct #: ");
                        var to = Console.ReadLine();
                        Console.Write("Amount: ");
                        var amt = decimal.Parse(Console.ReadLine());
                        svc.TransferAsync(from, to, amt);
                        Console.WriteLine($"From New Balance: {accountService.GetBalanceAsync(from)}");
                        Console.WriteLine($"To New Balance: {accountService.GetBalanceAsync(to)}");
                        break;
                    case "6"://Exit
                        Console.WriteLine("Exiting... the best system that you can have :)! Are you sure?");
                        var coomand =Console.ReadLine();
                        if(coomand== "yes")
                            break;
                        else
                        {
                            Console.WriteLine("Good choice, feel free to continue creating new Credit Accounts");
                            continue;
                        }
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}