
using System.Collections.Concurrent;
using TransactionSystem.DomainLogic.UserAdministration;
using TransactionSystem.Entityes;

namespace TransactionSystem.DomainLogic.AccountsService
{
    internal class AccountService : IAccountRepo
    {

        private readonly IUserAdministrationRepo _userAdministration;
        private readonly ConcurrentDictionary<string, Account> _accounts = new(); //instead Accounts Table

        public AccountService(IUserAdministrationRepo userAdministration)
        {
            _userAdministration = userAdministration;
        }
        /// <summary>
        /// From DDD point of view this functionality may be considered as a part from User/UserAdministraion Domain. 
        /// It depends from bussiness logic.For now I will leave it here
        /// </summary>
        public async Task CreateAccountAsync(User user, string username, string accountNumber, decimal initialBalance)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            await _userAdministration.AddUserAsync(user);

            var acccount = new Account(username, accountNumber, initialBalance);
            if (!_accounts.TryAdd(accountNumber, acccount))
                throw new InvalidOperationException("Account number already exists!!");

            user.AddAccount(acccount);
        }

        public Account GetAccount(string accountNumber)
        {

            if (_accounts.TryGetValue(accountNumber, out var acct))
                return acct;
            throw new InvalidOperationException("Account not found.");
        }

        public decimal GetBalance(string accountNumber)
        {
            return GetAccount(accountNumber).GetBalance();
        }
    }
}
