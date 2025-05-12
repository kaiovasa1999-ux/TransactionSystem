namespace TransactionSystem.DomainLogic.Transaction
{
    public interface ITransactionRepo
    {
        void Deposit(string accountNumber, decimal amount);

        void Withdraw(string accountNumber, decimal amount);

        void Transfer(string fromAccount, string toAccount, decimal amount);
    }
}