using Xunit;

namespace TransactionSystem.Test
{
    public class AccountTest
    {
        [Fact]
        public void NewAccount_HasInitialBalance()
        {
            var acct = new Account("pesho", "1234", 100m);
            Assert.Equal(100m, acct.GetBalance());
        }

        [Theory]
        [InlineData(50)]
        [InlineData(0.01)]
        public void Deposit_PositiveAmount_IncreasesBalance(decimal amount)
        {
            var acct = new Account("pesho", "1234", 100m);
            acct.Deposit(amount);
            Assert.Equal(100m + amount, acct.GetBalance());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Deposit_NegativeAmount_ThrowsArgumentException(decimal amount)
        {
            var acct = new Account("pesho", "1234", 100m);
            var ex = Assert.Throws<ArgumentException>(() => acct.Deposit(amount));
            Assert.Contains("must be positive", ex.Message);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(100)]
        public void Withdraw_ValidAmount_DecreasesBalance(decimal amount)
        {
            var acct = new Account("pesho", "1234", 100m);
            acct.Withdraw(amount);
            Assert.Equal(100m - amount, acct.GetBalance());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Withdraw_NonPositiveAmount_ThrowsArgumentException(decimal amount)
        {
            var acct = new Account("pesho", "1234", 100m);
            var ex = Assert.Throws<ArgumentException>(() => acct.Withdraw(amount));
            Assert.Contains("must be positive", ex.Message);
        }

        [Fact]
        public void Withdraw_MoreThanBalance_ThrowsInvalidOperationException()
        {
            var acct = new Account("pesho", "1234", 100m);
            Assert.Throws<InvalidOperationException>(() => acct.Withdraw(200m));
        }
    }
}
