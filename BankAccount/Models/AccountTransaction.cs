using BankAccountKata.Interfaces;

namespace BankAccountKata.Models
{
    public abstract class AccountTransaction(DateTime actionDate, decimal amount, decimal balance) : IAccountTransaction
    {
        public decimal Amount { get; } = amount;
        public DateTime ActionDateTime { get; } = actionDate;
        public decimal Balance { get; set; } = balance;

        public abstract decimal Execute();

        public abstract string Sign();
    }
}