using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TransactionSystem.DomainLogic.UserAdministration;
using TransactionSystem.Entityes;

namespace TransactionSystem.DomainLogic.AccountsService
{
    /// <summary>
    /// The same is with the AccountService. I decided to have a service/repo for the account For future 
    /// </summary>
    internal class AccountService : IAccountRepo
    {
        private readonly IUserAdministrationRepo _userAdministration;
        private readonly ConcurrentDictionary<string, Account> _accounts = new(); // instead of Accounts Table

        public AccountService(IUserAdministrationRepo userAdministration)
        {
            _userAdministration = userAdministration;
        }

        /// <summary>
        /// From DDD point of view this functionality may be considered as a part of User/UserAdministration Domain. 
        /// It depends on business logic. For now I will leave it here.
        /// </summary>
        public async Task CreateAccountAsync(User user, string accountName, string accountNumber, decimal initialBalance)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            await _userAdministration.AddUserAsync(user);

            var account = Account.CreateNewAccount(name: accountName, accountNumber: accountNumber, balance: initialBalance);

            if (!_accounts.TryAdd(accountNumber, account))
                throw new InvalidOperationException("Account number already exists!!");

            user.AddAccount(account);
        }

        public Task<Account> GetAccountAsync(string accountNumber)
        {
            if (_accounts.TryGetValue(accountNumber, out var account))
                return Task.FromResult(account);

            throw new InvalidOperationException("Account not found.");
        }

        public async Task<decimal> GetBalanceAsync(string accountNumber)
        {
            var account = await GetAccountAsync(accountNumber);
            return account.GetBalance();
        }
    }
}