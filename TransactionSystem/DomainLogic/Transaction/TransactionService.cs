using System;
using System.Threading;
using System.Threading.Tasks;
using TransactionSystem.DomainLogic.AccountsService;

namespace TransactionSystem.DomainLogic.Transaction
{
    public class TransactionService : ITransactionRepo
    {
        private readonly IAccountRepo _accountService;
        //private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public TransactionService(IAccountRepo accountService)
        {
            _accountService = accountService;
        }

        public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await _accountService.GetAccountAsync(accountNumber);
            account.Deposit(amount);
        }

        public async Task WithdrawAsync(string accountNumber, decimal amount)
        {
            var account = await _accountService.GetAccountAsync(accountNumber);
            account.Withdraw(amount);
        }

        public async Task TransferAsync(string fromAccount, string toAccount, decimal amount)
        {
            if (fromAccount == toAccount)
                throw new InvalidOperationException("Cannot transfer to the same account.");

            var sender = await _accountService.GetAccountAsync(fromAccount);
            var receiver = await _accountService.GetAccountAsync(toAccount);

            var locks = new[] { sender, receiver };
            Array.Sort(locks, (a, b) => string.Compare(a.AccountNumber, b.AccountNumber, StringComparison.Ordinal));

            
            // Using SemaphoreSlim to support async locking.
            // Just to show you that you can use it as a variant if you want to.


            //await _semaphore.WaitAsync();
            //try
            //{
            lock (locks[0])
                {
                    lock (locks[1])
                    {
                        sender.Withdraw(amount);
                        receiver.Deposit(amount);
                    }
                }
            //}
            //finally
            //{
            //    _semaphore.Release();
            //}
        }
    }
}
