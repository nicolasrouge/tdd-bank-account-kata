namespace BankAccountKata.Interfaces;

public interface IAccountTransaction
{
    decimal Amount { get; }
    DateTime ActionDateTime { get; }
    decimal Balance { get; set; }

    decimal Execute();

    string Sign();
}