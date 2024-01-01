using BankAccountKata.Interfaces;

namespace BankAccountKata.Models.Transactions;

public class WithdrawTransaction(DateTime actionDate, decimal amount, decimal balance, ITransactionFeeStrategy feeStrategy)
    : AccountTransaction(actionDate, amount, balance)
{

    private readonly ITransactionFeeStrategy _feeStrategy = feeStrategy;

    public override decimal Execute()
    {
        var fee = _feeStrategy.CalculateFee(Amount);
        Balance -= Amount + fee;
        return Balance;
    }

    public override string Sign() => "-";
}