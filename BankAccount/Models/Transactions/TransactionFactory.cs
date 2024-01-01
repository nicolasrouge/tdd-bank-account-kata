using BankAccountKata.Interfaces;

namespace BankAccountKata.Models.Transactions;

public class TransactionFactory : ITransactionFactory
{
    public IAccountTransaction CreateDepositTransaction(decimal amount, decimal balance)
    {
        IAccountTransaction deposit = new DepositTransaction(DateTime.Now, amount, balance);
        return deposit;
    }

    public IAccountTransaction CreateWithdrawTransaction(decimal amount, decimal balance, ITransactionFeeStrategy feeStrategy)
    {
        IAccountTransaction withdraw = new WithdrawTransaction(DateTime.Now, amount, balance, feeStrategy);
        return withdraw;
    }
}