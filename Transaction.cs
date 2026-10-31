namespace BudgetTracker;

public enum TransactionType { Income, Expense }

public class Transaction
{
    public int Id { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string Category { get; private set; }

    public Transaction(int id, TransactionType type, decimal amount, string category)
        : this(id, type, amount, category, DateTime.Now)
    {
    }
    public Transaction(int id, TransactionType type, decimal amount,
                   string category, DateTime date)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),
            "Amount must be greater than zero");
        }
        else if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Category cannot be empty or whitespace",
            nameof(category));
        }
        Id = id;
        Type = type;
        Amount = amount;
        Category = category;
        Date = date;

    }
}