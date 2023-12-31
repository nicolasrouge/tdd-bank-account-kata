namespace BankAccountKata.Models;

public class WithdrawTransaction(DateTime actionDate, decimal amount, decimal balance)
    : AccountTransaction(actionDate, amount, balance)
{
    public override decimal Execute()
    {
        Balance -= Amount;
        return Balance;
    }

    public override string Sign() => "-";
}