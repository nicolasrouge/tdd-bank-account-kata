using System.Text;
using FluentAssertions;

namespace BankAccountKata.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void BankAccount_InitializedWithCorrectBalance_ShouldReflectCorrectBalance()
        {
            // Arrange
            const decimal initialBalance = 100.0m;

            // Act
            var account = new BankAccountKata.Models.BankAccount(initialBalance);

            // Assert
            account.Balance.Should().Be(initialBalance,
                because: "the balance should be equal to the initial balance provided.");
        }

        [Fact]
        public void Deposit_AmountIncreasesBalance_ShouldReflectNewBalance()
        {
            // Arrange
            const decimal initialBalance = 100.0m;
            const decimal depositAmount = 50.0m;
            var account = new BankAccountKata.Models.BankAccount(initialBalance);

            // Act
            account.Deposit(depositAmount);

            // Assert
            account.Balance.Should().Be(initialBalance + depositAmount,
                because: "depositing money should increase the balance by the deposited amount.");
        }

        [Fact]
        public void Withdraw_AmountDecreasesBalance_ShouldReflectNewBalance()
        {
            // Arrange
            const decimal initialBalance = 100.0m;
            const decimal withdrawalAmount = 30.0m;
            var account = new BankAccountKata.Models.BankAccount(initialBalance);

            // Act
            account.Withdraw(withdrawalAmount);

            // Assert
            account.Balance.Should().Be(initialBalance - withdrawalAmount,
                because: "withdrawing money should decrease the balance by the withdrawn amount.");
        }

        [Fact]
        public void Deposit_NegativeAmount_ThrowsArgumentException()
        {
            // Arrange
            var account = new BankAccountKata.Models.BankAccount(100.0m);
            var negativeAmount = -50.0m;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Deposit(negativeAmount));
            Assert.Equal("Deposit amount must be positive", exception.Message);
        }

        [Fact]
        public void Withdraw_NegativeAmount_ThrowsArgumentException()
        {
            // Arrange
            var account = new BankAccountKata.Models.BankAccount(100.0m);
            var negativeAmount = -50.0m;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Withdraw(negativeAmount));
            Assert.Equal("Withdrawal amount must be positive.", exception.Message);
        }

        [Fact]
        public void Withdraw_AmountExceedingBalance_ThrowsInvalidOperationException()
        {
            // Arrange
            var account = new BankAccountKata.Models.BankAccount(100.0m);
            var excessiveAmount = 150.0m;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => account.Withdraw(excessiveAmount));
            Assert.Equal("Insufficient funds for the withdrawal.", exception.Message);
        }

        [Fact]
        public void PrintStatement_ReturnsFormattedStatementWithTransactions_AtSameDate()
        {
            // Arrange
            const decimal initialBalance = 100.0m;
            const decimal withdrawalAmount1 = 30.0m;
            const decimal withdrawalAmount2 = 10.0m;
            var account = new BankAccountKata.Models.BankAccount(initialBalance);
            account.Withdraw(withdrawalAmount1);
            account.Withdraw(withdrawalAmount2);
            var today = DateTime.Now.ToShortDateString();

            // Act
            var statement = account.PrintStatement();

            // Assert
            var expectedStatement = new StringBuilder();
            expectedStatement.AppendLine("Date\tAmount\tBalance");
            expectedStatement.AppendLine($"{today}\t-30.00\t70.00");
            expectedStatement.AppendLine($"{today}\t-10.00\t60.00");
            Assert.Equal(expectedStatement.ToString(), statement);
        }
    }
}