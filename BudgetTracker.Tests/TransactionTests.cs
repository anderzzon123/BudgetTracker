using BudgetTracker;

namespace BudgetTracker.Tests;

public class TransactionTests
{
    [Fact]
    public void Constructor_NegativeAmount_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Transaction t = new Transaction(1, TransactionType.Expense, -500, "Mat");
        });
    }

    [Fact]
    public void Constructor_ValidValues_CreatesTransaction()
    {
        Transaction t = new Transaction(1, TransactionType.Expense, 500, "Mat");
        Assert.Equal(500m, t.Amount); 
        Assert.Equal("Mat", t.Category);
        Assert.Equal(TransactionType.Expense, t.Type);

    }

    [Fact]
    public void Update_ValidValues_UpdatesTransaction()
    {
        Transaction t = new Transaction(1, TransactionType.Expense, 500, "Mat");
        t.Update(750, "Transport", TransactionType.Income);
        Assert.Equal(750m, t.Amount);
        Assert.Equal("Transport", t.Category);
        Assert.Equal(TransactionType.Income, t.Type);
    }

    [Fact]
    public void Update_NegativeAmount_ThrowsException()
    {
        Transaction t = new Transaction(1, TransactionType.Expense, 500, "Mat");
        // Försök uppdatera till -100 och kontrollera undantaget.
        // Kontrollera sedan att beloppet fortfarande är 500.
        Assert.Throws<ArgumentException>(() =>
        {
            t.Update(-100, "Mat", TransactionType.Income);
        });
        Assert.Equal(500m, t.Amount);

    }
}