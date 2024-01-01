namespace BankAccountKata.Interfaces
{
    public interface ITransactionFeeStrategy
    {
        decimal CalculateFee(decimal amount);
    }
}