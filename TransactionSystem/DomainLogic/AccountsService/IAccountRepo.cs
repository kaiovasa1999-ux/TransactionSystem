using TransactionSystem.Entityes;

namespace TransactionSystem.DomainLogic.AccountsService
{
    public interface IAccountRepo
    {
        Task CreateAccountAsync(User user, string username, string accountNumber, decimal initialBalance);

        decimal GetBalance(string accountNumber);

        Account GetAccount(string accountNumber);
    }
}
