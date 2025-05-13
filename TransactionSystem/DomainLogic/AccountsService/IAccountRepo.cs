using TransactionSystem.Entityes;

namespace TransactionSystem.DomainLogic.AccountsService
{
    public interface IAccountRepo
    {
        Task CreateAccountAsync(User user, string username, string accountNumber, decimal initialBalance);

        Task<decimal> GetBalanceAsync(string accountNumber);

        Task<Account> GetAccountAsync(string accountNumber);
    }
}
