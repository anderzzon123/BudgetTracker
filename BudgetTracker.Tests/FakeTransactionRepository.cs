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
    public void DeleteTransaction(int id)
    {
        Transaction? transaction = transactions.FirstOrDefault(t => t.Id == id);
        if (transaction != null)
        {
            transactions.Remove(transaction);
        }
    }
    public List<Transaction> GetAllTransactionsByType(TransactionType type)
    {
        return transactions.Where(t => t.Type == type).ToList();
    }
    public void UpdateTransaction(int id, decimal newPrice, string newCategory, TransactionType newType)
    {
        Transaction? transaction = transactions.FirstOrDefault(t => t.Id == id);

        if (transaction != null)
        {
            transaction.Update(newPrice, newCategory, newType);

        }
    }
    public Transaction? GetTransactionById(int id)
    {
        Transaction? transaction = transactions.FirstOrDefault(t => t.Id == id);
        return transaction;
    }

}