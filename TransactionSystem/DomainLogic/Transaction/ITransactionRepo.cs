namespace TransactionSystem.DomainLogic.Transaction
{
    public interface ITransactionRepo
    {
        Task DepositAsync(string accountNumber, decimal amount);

        Task WithdrawAsync(string accountNumber, decimal amount);

        Task TransferAsync(string fromAccount, string toAccount, decimal amount);
    }
}