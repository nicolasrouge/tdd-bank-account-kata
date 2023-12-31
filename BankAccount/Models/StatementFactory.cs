using System.Text;

namespace BankAccountKata.Models;

public class StatementFactory
{
    public static string GenerateStatementFrom(IEnumerable<IAccountTransaction> transactions)
    {
        var statement = new StringBuilder();
        statement.AppendLine("Date\tAmount\tBalance");
        foreach (var transaction in transactions)
        {
            statement.AppendLine(
                $"{transaction.ActionDateTime.ToShortDateString()}\t{transaction.Sign()}{transaction.Amount:F2}\t{transaction.Balance:F2}");
        }

        return statement.ToString();
    }
}