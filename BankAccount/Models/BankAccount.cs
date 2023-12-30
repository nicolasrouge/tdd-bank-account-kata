using System.Text;

namespace BankAccountKata.Models
{
    public class BankAccount(decimal initialBalance)
    {
        public decimal Balance { get; private set; } = initialBalance;
        public List<AccountTransaction> Transactions = [];

        public string PrintStatement()  
        {
            var statement = new StringBuilder();
            statement.AppendLine("Date\tAmount\tBalance");
            foreach (var transaction in Transactions)
            {
                statement.AppendLine(
                    $"{transaction.ActionDate.ToShortDateString()}\t{transaction.Amount:+0.00;-0.00;0}\t{transaction.FormattedBalance}");
            }

            return statement.ToString();
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                var registry = new AccountTransaction(DateTime.Now, amount, Balance);
                Transactions.Add(registry);
            }
            else
            {
                throw new ArgumentException("Deposit amount must be positive");
            }
        }

        public void Withdraw(decimal amount)
        {
            switch (amount)
            {
                case <= 0:
                    throw new ArgumentException("Withdrawal amount must be positive.");
                case > 0 when Balance >= amount:
                    {
                        Balance -= amount;
                        var registry = new AccountTransaction(DateTime.Now, -amount, Balance);
                        Transactions.Add(registry);
                        break;
                    }
                default:
                    throw new InvalidOperationException("Insufficient funds for the withdrawal.");
            }
        }
    }
}