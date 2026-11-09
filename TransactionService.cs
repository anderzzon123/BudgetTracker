namespace BudgetTracker;

public class TransactionService
{
    private readonly ITransactionRepository repository;

    public TransactionService(ITransactionRepository repository)
    {
        this.repository = repository;
    }

    public void AddTransaction(Transaction transaction)
    {
        repository.SaveTransaction(transaction);
    }

    public List<Transaction> GetAllTransactions()
    {
        return repository.GetAllTransactions();
    }

    public void DeleteTransaction(int id)
    {
        repository.DeleteTransaction(id);
    }
    public void UpdateTransaction(int id, decimal amount, string newCategory, TransactionType newType)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Beloppet måste vara större än 0.");
        }
        if (string.IsNullOrWhiteSpace(newCategory))
        {
            throw new ArgumentException("Måste finnas categori");
        }
        if (newType != TransactionType.Expense && newType != TransactionType.Income)
        {
            throw new ArgumentException("Måste ha en typ.");
        }
        repository.UpdateTransaction(id, amount, newCategory, newType);
    }
    public decimal GetBalance()
    {
        List<Transaction> transactions = repository.GetAllTransactions();
        decimal balance = 0;

        foreach (Transaction transaction in transactions)
        {
            if (transaction.Type == TransactionType.Income)
            {
                balance += transaction.Amount;
            }
            else
            {
                balance -= transaction.Amount;
            }

        }
        return balance;


    }

}