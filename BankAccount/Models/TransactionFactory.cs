using BankAccountKata.Interfaces;

namespace BankAccountKata.Models;

public class TransactionFactory : ITransactionFactory
{
    public IAccountTransaction CreateDepositTransaction(decimal amount, decimal balance)
    {
        IAccountTransaction deposit = new DepositTransaction(DateTime.Now, amount, balance);
        return deposit;
    }

    public IAccountTransaction CreateWithdrawTransaction(decimal amount, decimal balance)
    {
        IAccountTransaction withdraw = new WithdrawTransaction(DateTime.Now, amount, balance);
        return withdraw;
    }
}