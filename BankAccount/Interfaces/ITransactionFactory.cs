namespace BankAccountKata.Interfaces;

public interface ITransactionFactory
{
    public IAccountTransaction CreateDepositTransaction(decimal amount, decimal balance);

    public IAccountTransaction CreateWithdrawTransaction(decimal amount, decimal balance, ITransactionFeeStrategy feeStrategy);
}