using BudgetTracker;

Transaction t1 = new Transaction(1, TransactionType.Income, 22400, "Lön");

TransactionRepository repository = new();

repository.SaveTransaction(t1);