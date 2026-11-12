namespace BudgetTracker;

public interface ITransactionRepository
{
    void SaveTransaction(Transaction transaction);

    List<Transaction> GetAllTransactions();

    List<Transaction> GetAllTransactionsByType(TransactionType type);

    void DeleteTransaction(int id);

    void UpdateTransaction(int id, decimal amount, string newCategory, TransactionType newType);

}

