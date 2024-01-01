using BankAccountKata.Interfaces;

namespace BankAccountKata.Models.Fees
{
    public class FixedFeeStrategy(decimal fixedFee) : ITransactionFeeStrategy
    {
        public decimal CalculateFee(decimal amount)
        {
            return fixedFee;
        }
    }

    public class PercentageFeeStrategy(decimal percentage) : ITransactionFeeStrategy
    {
        public decimal CalculateFee(decimal amount)
        {
            return amount * percentage;
        }
    }

    public class NoFeeStrategy : ITransactionFeeStrategy
    {
        public decimal CalculateFee(decimal amount)
        {
            return 0;
        }
    }
}