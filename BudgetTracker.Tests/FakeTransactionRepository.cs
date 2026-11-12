namespace BudgetTracker.Tests;

using BudgetTracker;

public class FakeTransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> transactions = new();

    public List<Transaction> GetAllTransactions()
    {
        return transactions;
    }
    public void SaveTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
    }

}