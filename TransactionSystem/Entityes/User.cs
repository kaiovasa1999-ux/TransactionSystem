namespace TransactionSystem.Entityes
{
    public class User
    {
        private readonly object _lock = new object();

        public User(string name)
        {
            Name = name;
            Accunts = new List<Account>();
        }
        public string Name { get; set; }

        public List<Account> Accunts { get; set; }

        public void AddAccount(Account account)
        {
            lock (_lock) 
            {
                if (account == null)
                    throw new ArgumentNullException(nameof(account), "Account cannot be null.");
                if (Accunts.Any(a => a.AccountNumber == account.AccountNumber))
                    throw new InvalidOperationException("Account number already exists.");

                Accunts.Add(account);
            }
        }
    }
}
