namespace TransactionSystem.Entityes
{
    public class Account
    {
        private string _name;
        private string _accountNumber;
        private decimal _balance;

        /// <summary>
        /// I am useing this field because every insatnce of account well have a different
        /// lock object (will point to a different memory location) and this will help me
        /// to avoid deadlocks (not waiting each other to do the give operation)
        /// </summary>
        private readonly object _lock = new object();
        public Account()
        {
            
        }

        public static Account CreateNewAccount(string name, string accountNumber, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null, empty, or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number cannot be null, empty, or whitespace.", nameof(accountNumber));
            if (balance < 0)
                throw new ArgumentOutOfRangeException(nameof(balance), "Balance cannot be negative.");

            Account account = new Account();
            account._name = name;
            account._accountNumber = accountNumber;
            account._balance = balance;
            account.IsActive = true;

            return account;
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Name cannot be null, empty, or whitespace.", nameof(value));

                _name = value;
            }
        }
        public string AccountNumber
        {
            get => _accountNumber;
            private set
            {

                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Account number cannot be null, empty, or whitespace.",nameof(value));

                _accountNumber = value;
            }
        }
        public decimal Balance
        {
            get => _balance;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Balance cannot be negative.");
                _balance = value;
            }
        }

        public bool IsActive { get; private set; }

        public decimal GetBalance()
        {
            EnsureAccountIsActive();
            lock (_lock)
            {
                return _balance;
            }
        }

        public void Deposit(decimal amount)
        {
            EnsureAccountIsActive();
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.", nameof(amount));

            lock (_lock)
            {
                _balance += amount;
            }
        }

        public void Withdraw(decimal amount)
        {
            EnsureAccountIsActive();
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));

            lock (_lock)
            {
                if (amount > _balance)
                    throw new InvalidOperationException("Insufficient funds.");
                _balance -= amount;
            }
        }

        private void EnsureAccountIsActive()
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Account is closed");
            }
        }
    }
}