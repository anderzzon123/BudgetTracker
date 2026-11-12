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
}