﻿using Moq;
using TransactionSystem.DomainLogic.AccountsService;
using TransactionSystem.DomainLogic.Transaction;

namespace System.Test
{
    public class TransactionServiceTests
    {
        private readonly Mock<IAccountRepo> _accountServiceMock;
        private readonly TransactionService _transferService;

        public TransactionServiceTests()
        {
            _accountServiceMock = new Mock<IAccountRepo>(MockBehavior.Strict);
            _transferService = new TransactionService(_accountServiceMock.Object);
        }

        [Fact]

        public async Task Transfer_To_Same_Account_ThrowsInvalidOperationException()
        {
            //Arrange
            var accountNumber = "123456789";
            // act & assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _transferService.TransferAsync(accountNumber, accountNumber, 100));

            Assert.Equal("Cannot transfer to the same account.", ex.Message);
        }
    }
}
