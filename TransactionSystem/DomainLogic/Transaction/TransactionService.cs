using System.Threading.Tasks;
using TransactionSystem.DomainLogic.AccountsService;

namespace TransactionSystem.DomainLogic.Transaction
{
    public class TransactionService : ITransactionRepo
    {
        private readonly IAccountRepo _accountService;
        public TransactionService(IAccountRepo accountService)
        {
            _accountService = accountService;
        }
        public void Deposit(string accountNumber, decimal amount)
        {
            var acccount = _accountService.GetAccount(accountNumber);
            acccount.Deposit(amount);
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            var acccount = _accountService.GetAccount(accountNumber);
            acccount.Withdraw(amount);
        }

        public void Transfer(string fromAccount, string toAccount, decimal amount)
        {
            if (fromAccount == toAccount)
                throw new InvalidOperationException("Cannot transfer to the same account.");

            var sender = _accountService.GetAccount(fromAccount);
            var recievert = _accountService.GetAccount(toAccount);

            // lock both accounts in a consistent order to avoid deadlocks (not waithing each othere to do the givven operation)
            var locks = new[] { sender, recievert };
            Array.Sort(locks, (a, b) => string.Compare(a.AccountNumber, b.AccountNumber, StringComparison.Ordinal));

            lock (locks[0])
            {
                lock (locks[1])
                {
                    sender.Withdraw(amount);
                    recievert.Deposit(amount);
                }
            }
        }
    }
}
