using System.Text;
using BankAccountKata.Models;
using BankAccountKata.Models.Fees;
using BankAccountKata.Models.Transactions;
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
            var account = new BankAccount(initialBalance, new TransactionFactory());

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
            var account = new BankAccount(initialBalance, new TransactionFactory());

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
            var account = new BankAccount(initialBalance, new TransactionFactory());

            // Act
            account.Withdraw(withdrawalAmount, new NoFeeStrategy());

            // Assert
            account.Balance.Should().Be(initialBalance - withdrawalAmount,
                because: "withdrawing money should decrease the balance by the withdrawn amount.");
        }

        [Fact]
        public void Deposit_NegativeAmount_ThrowsArgumentException()
        {
            // Arrange
            var account = new BankAccount(100.0m, new TransactionFactory());
            var negativeAmount = -50.0m;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Deposit(negativeAmount));
            Assert.Equal("Deposit amount must be positive", exception.Message);
        }

        [Fact]
        public void Withdraw_NegativeAmount_ThrowsArgumentException()
        {
            // Arrange
            var account = new BankAccount(100.0m, new TransactionFactory());
            var negativeAmount = -50.0m;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Withdraw(negativeAmount, new FixedFeeStrategy(0)));
            Assert.Equal("Withdrawal amount must be positive.", exception.Message);
        }

        [Fact]
        public void Withdraw_AmountExceedingBalance_ThrowsInvalidOperationException()
        {
            // Arrange
            var account = new BankAccount(100.0m, new TransactionFactory());
            var excessiveAmount = 150.0m;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => account.Withdraw(excessiveAmount, new FixedFeeStrategy(0)));
            Assert.Equal("Insufficient funds for the withdrawal.", exception.Message);
        }

        [Fact]
        public void PrintStatement_ReturnsFormattedStatementWithTransactions_AtSameDate()
        {
            // Arrange
            const decimal initialBalance = 100.0m;
            const decimal withdrawalAmount1 = 30.0m;
            const decimal withdrawalAmount2 = 10.0m;
            var account = new BankAccount(initialBalance, new TransactionFactory());
            account.Withdraw(withdrawalAmount1, new NoFeeStrategy());
            account.Withdraw(withdrawalAmount2, new NoFeeStrategy());
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

        [Fact]
        public void Withdraw_WithFixedFee_DeductsAmountAndFee()
        {
            // Arrange
            const decimal initialBalance = 200.0m;
            const decimal withdrawalAmount = 50.0m;
            const decimal fixedFee = 10.0m;
            var account = new BankAccount(initialBalance, new TransactionFactory());
            var fixedFeeStrategy = new FixedFeeStrategy(fixedFee);

            // Act
            account.Withdraw(withdrawalAmount, fixedFeeStrategy);

            // Assert
            var expectedBalance = initialBalance - withdrawalAmount - fixedFee;
            account.Balance.Should().Be(expectedBalance,
                because: "the balance should decrease by the withdrawal amount and the fixed fee.");
        }

        [Fact]
        public void Withdraw_WithPercentageFee_DeductsAmountAndCalculatedFee()
        {
            // Arrange
            const decimal initialBalance = 500.0m;
            const decimal withdrawalAmount = 100.0m;
            const decimal percentageFee = 0.05m;
            var account = new BankAccount(initialBalance, new TransactionFactory());
            var percentageFeeStrategy = new PercentageFeeStrategy(percentageFee);

            // Act
            account.Withdraw(withdrawalAmount, percentageFeeStrategy);

            // Assert
            var expectedFee = withdrawalAmount * percentageFee;
            var expectedBalance = initialBalance - withdrawalAmount - expectedFee;
            account.Balance.Should().Be(expectedBalance,
                because: "the balance should decrease by the withdrawal amount and the calculated percentage fee.");
        }



    }
}