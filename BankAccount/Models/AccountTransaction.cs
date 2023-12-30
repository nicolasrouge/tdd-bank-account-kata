using System.Globalization;

namespace BankAccountKata.Models;

public class AccountTransaction(DateTime actionDate, decimal amount, decimal balance)
{
    public readonly DateTime ActionDate = actionDate;
    public readonly decimal Amount = amount;
    public readonly decimal Balance = balance;

    public string FormattedBalance => Balance.ToString("F2", CultureInfo.InvariantCulture);
}