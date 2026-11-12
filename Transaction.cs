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


    // metod för att uppdatera en transaktion
    public void Update(decimal newPrice, string newCategory, TransactionType newType)
    {
        if (newPrice <= 0)
        {
            throw new ArgumentException("Beloppet måste vara större än 0");
        }
        if (string.IsNullOrWhiteSpace(newCategory))
        {
            throw new ArgumentException("Måste ha en kategori");
        }
        if (newType != TransactionType.Income && newType != TransactionType.Expense)
        {
            throw new ArgumentException("Ogiltlig transaktionstyp");
        }
        Amount = newPrice;
        Category = newCategory;
        Type = newType;
    }
}