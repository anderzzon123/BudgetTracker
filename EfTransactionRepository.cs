namespace BudgetTracker;

public class EfTransactionRepository : ITransactionRepository
{
    private readonly BudgetDbContext context;

    public EfTransactionRepository(BudgetDbContext context)
    {
        this.context = context;
    }

    public List<Transaction> GetAllTransactions()
    {
        return context.Transactions.ToList();
    }
    public void SaveTransaction(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        context.SaveChanges();
    }
    public void DeleteTransaction(int id)
    {
        Transaction? transaction = context.Transactions.Find(id);
        if (transaction != null)
        {
            context.Transactions.Remove(transaction);
            context.SaveChanges();
        }
    }
    public void UpdateTransaction(int id, decimal newPrice, string newCategory,
    TransactionType newType)
    {
        Transaction? transaction = context.Transactions.Find(id);

        if (transaction != null)
        {
            transaction.Update(newPrice, newCategory, newType);
            context.SaveChanges();
        }
    }
    public List<Transaction> GetAllTransactionsByType(TransactionType type)
    {
        return context.Transactions.Where(t => t.Type == type).ToList();
    }
    public Transaction? GetTransactionById(int id)
    {
        Transaction? transaction = context.Transactions.Find(id);
        return transaction;
    }
}