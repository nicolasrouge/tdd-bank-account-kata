namespace BankAccountKata.Models
{
    public class BankAccount(decimal initialBalance)
    {
        public decimal Balance { get; private set; } = initialBalance;
        public List<IAccountTransaction> Transactions = [];

        public string PrintStatement() => StatementFactory.GenerateStatementFrom(Transactions);

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                IAccountTransaction deposit = new DepositTransaction(DateTime.Now, amount, Balance);
                Balance = deposit.Execute();
                Transactions.Add(deposit);
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
                        IAccountTransaction withdraw = new WithdrawTransaction(DateTime.Now, amount, Balance);
                        Balance = withdraw.Execute();

                        Transactions.Add(withdraw);
                        break;
                    }
                default:
                    throw new InvalidOperationException("Insufficient funds for the withdrawal.");
            }
        }
    }
}