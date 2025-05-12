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

        public Account(string name, string accountNumber, decimal initialBalance)
        {
            _name = name;
            _accountNumber = accountNumber;
            _balance = initialBalance;
        }

        public string Name
        {
            get => _name;
            init
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
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Account number cannot be null, empty, or whitespace.",nameof(value));

                _accountNumber = value;
            }
        }

        public decimal GetBalance()
        {
            lock (_lock)
            {
                return _balance;
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.", nameof(amount));

            lock (_lock)
            {
                _balance += amount;
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));

            lock (_lock)
            {
                if (amount > _balance)
                    throw new InvalidOperationException("Insufficient funds.");
                _balance -= amount;
            }
        }
    }
}