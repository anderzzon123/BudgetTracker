namespace BudgetTracker;

public class BudgetManager
{
    private List<Transaction> transactions = new();



    public void AddTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
    }
    public IEnumerable<Transaction> GetTransactions()
    {
        return transactions;
    }
    public decimal GetBalance()
    {
        decimal balance = 0m;
        foreach (Transaction t in transactions)
        {
            if (t.Type == TransactionType.Income)
            {
                balance += t.Amount;
            }
            else
            {
                balance -= t.Amount;
            }
        }
        return balance;

    }
}