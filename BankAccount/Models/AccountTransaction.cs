namespace BankAccountKata.Models
{
    public interface IAccountTransaction
    {
        decimal Amount { get; }
        DateTime ActionDateTime { get; }
        decimal Balance { get; set; }

        decimal Execute();

        string Sign();
    }

    public abstract class AccountTransaction : IAccountTransaction
    {
        public decimal Amount { get; }
        public DateTime ActionDateTime { get; }
        public decimal Balance { get; set; }

        protected AccountTransaction(DateTime actionDate, decimal amount, decimal balance)
        {
            Amount = amount;
            ActionDateTime = actionDate;
            Balance = balance;
        }

        public abstract decimal Execute();

        public abstract string Sign();
    }

    public class DepositTransaction : AccountTransaction
    {
        public DepositTransaction(DateTime actionDate, decimal amount, decimal balance)
            : base(actionDate, amount, balance)
        {
        }

        public override decimal Execute()
        {
            Balance += Amount;
            return Balance;
        }

        public override string Sign() => "+";
    }

    public class WithdrawTransaction : AccountTransaction
    {
        public WithdrawTransaction(DateTime actionDate, decimal amount, decimal balance)
            : base(actionDate, amount, balance)
        {
        }

        public override decimal Execute()
        {
            Balance -= Amount;
            return Balance;
        }

        public override string Sign() => "-";
    }
}