using BudgetTracker;

namespace BudgetTracker.Tests;

public class TransactionServiceTests
{
    [Fact]
    public void AddTransaction_ValidTransaction_SavesTransaction()
    {
        FakeTransactionRepository repository = new();
        TransactionService transactionService = new TransactionService(repository);
        Transaction transaction = new Transaction(1, TransactionType.Expense, 500, "Mat");
        transactionService.AddTransaction(transaction);
        Assert.Equal(1, repository.GetAllTransactions().Count);
    }

    [Fact]
    public void GetBalance_MultipleTransactions_ReturnsCorrectBalance()
    {
        FakeTransactionRepository repository = new();
        TransactionService transactionService = new(repository);
        Transaction t1 = new Transaction(1, TransactionType.Income, 20000, "Lön");
        Transaction t2 = new Transaction(2, TransactionType.Expense, 5000, "Hyra");
        Transaction t3 = new Transaction(3, TransactionType.Expense, 2000, "Mat");
        transactionService.AddTransaction(t1);
        transactionService.AddTransaction(t2);
        transactionService.AddTransaction(t3);
        Assert.Equal(13000m, transactionService.GetBalance());
    }
    [Fact]
    public void DeleteTransaction_ExistingTransaction_RemovesTransaction()
    {
        FakeTransactionRepository repository = new();
        TransactionService transactionService = new(repository);
        Transaction t1 = new Transaction(1, TransactionType.Income, 20000, "Lön");
        Transaction t2 = new Transaction(2, TransactionType.Expense, 5000, "Hyra");
        transactionService.AddTransaction(t1);
        transactionService.AddTransaction(t2);
        transactionService.DeleteTransaction(t1.Id);
        List<Transaction> remaining = transactionService.GetAllTransactions();
        Assert.Single(remaining);
        Assert.Equal(2, remaining[0].Id);
    }
    [Fact]
    public void UpdateTransaction_ValidValues_UpdatesTransaction()
    {
        FakeTransactionRepository repository = new();
        TransactionService transactionService = new(repository);
        Transaction t1 = new Transaction(1, TransactionType.Expense, 500, "Mat");
        transactionService.AddTransaction(t1);
        transactionService.UpdateTransaction(1, 800, "Transport", TransactionType.Expense);

        // t1.Update(800, "Transport", TransactionType.Expense);
        Assert.Equal(800m, t1.Amount);
        Assert.Equal("Transport", t1.Category);
    }


}