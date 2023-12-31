using BankAccountKata.Interfaces;

namespace BankAccountKata.Models
{
    public class BankAccount(decimal initialBalance, ITransactionFactory transactionFactory)
    {
        public decimal Balance { get; private set; } = initialBalance;
        public List<IAccountTransaction> Transactions = [];

        public string PrintStatement() => StatementFactory.GenerateStatementFrom(Transactions);

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }

            var deposit = transactionFactory.CreateDepositTransaction(amount, Balance);
            Balance = deposit.Execute();
            Transactions.Add(deposit);
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }

            if (Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds for the withdrawal.");
            }

            var withdraw = transactionFactory.CreateWithdrawTransaction(amount, Balance);
            Balance = withdraw.Execute();
            Transactions.Add(withdraw);
        }
    }
}