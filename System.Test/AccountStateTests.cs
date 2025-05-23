using TransactionSystem.Entityes;

namespace TransactionSystem.Test
{
    public class AccountStateTests
    {
        /// <summary>
        /// This test are validating the end state of the account. 
        /// </summary>
        [Fact]
        public void NewAccount_HasInitialBalance()
        {
            //arrange
            var acct = Account.CreateNewAccount("Andriyan Krastev", "1234", 1000000000m);
            //assert
            Assert.Equal(1000000000m, acct.GetBalance());
        }

        [Theory]
        [InlineData(50)]
        [InlineData(0.01)]
        public void Deposit_PositiveAmount_IncreasesBalance(decimal amount)
        {
            //arange
            var acct = Account.CreateNewAccount("Andriyan Krastev", "1234", 1000000000m);
            //act
            acct.Deposit(amount);
            //assert
            Assert.Equal(1000000000m + amount, acct.GetBalance());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Deposit_NegativeAmount_ThrowsArgumentException(decimal amount)
        {
            //arrange
            var acct = Account.CreateNewAccount("Andriyan Krastev", "1234", 1000000000m);
            //act
            var ex = Assert.Throws<ArgumentException>(() => acct.Deposit(amount));
            //assert 
            Assert.Contains("must be positive", ex.Message);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(100)]
        public void Withdraw_ValidAmount_DecreasesBalance(decimal amount)
        {
            //arrage
            var acct = Account.CreateNewAccount("Andriyan Krastev", "1234", 1000000000m);
            //act
            acct.Withdraw(amount);
            //asert
            Assert.Equal(1000000000m - amount, acct.GetBalance());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Withdraw_NonPositiveAmount_ThrowsArgumentException(decimal amount)
        {

            //araneg
            var acct = Account.CreateNewAccount("Andriyan Krastev", "1234", 1000000000m);
            //act
            var ex = Assert.Throws<ArgumentException>(() => acct.Withdraw(amount));
            //assert
            Assert.Contains("must be positive", ex.Message);
        }

        [Fact]
        public void Withdraw_MoreThanBalance_ThrowsInvalidOperationException()
        {
            var acct = Account.CreateNewAccount("pesho", "1234", 101m);
            Assert.Throws<InvalidOperationException>(() => acct.Withdraw(1012m));
        }
    }
}
