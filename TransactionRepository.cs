using Microsoft.Data.Sqlite;

namespace BudgetTracker;

public class TransactionRepository : ITransactionRepository
{
    private string connectionString = "Data Source=../budget.db";


    public void TestConnection()
    {
        using SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        Console.WriteLine("Connected to SQLite!");
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText =
            "SELECT COUNT(*) FROM Transactions";

        object? result = command.ExecuteScalar();

        Console.WriteLine($"Number of transactions: {result}");
    }

    public void SaveTransaction(Transaction transaction)
    {
        using SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
    INSERT INTO Transactions (Type, Amount, Category, Date)
    VALUES (@type, @amount, @category, @date);
    """;
        command.Parameters.AddWithValue("@type", transaction.Type.ToString());
        command.Parameters.AddWithValue("@amount", decimal.ToInt64(transaction.Amount * 100));
        command.Parameters.AddWithValue("@category", transaction.Category);
        command.Parameters.AddWithValue("@date", transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        command.ExecuteNonQuery();
    }

    public List<Transaction> GetAllTransactions()
    {
        using SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText =
            "SELECT * FROM Transactions";
        List<Transaction> transactions = new();
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string category = reader.GetString(3);
            TransactionType type = Enum.Parse<TransactionType>(reader.GetString(1));
            decimal amount = reader.GetDecimal(2) / 100m;
            DateTime date = DateTime.Parse(reader.GetString(4));

            Transaction transaction = new Transaction(id, type, amount, category, date);
            transactions.Add(transaction);
        }
        return transactions;
    }
    public void DeleteTransaction(int id)
    {
        using SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText =
            "DELETE FROM Transactions WHERE Id = @id";

        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
    public void UpdateTransaction(int id, decimal newAmount, string newCategory, TransactionType newType)
    {
        using SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText =
            "UPDATE Transactions SET Amount = @amount, Category = @category, Type = @type WHERE Id = @id";

        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@amount", decimal.ToInt64(newAmount * 100));
        command.Parameters.AddWithValue("@category", newCategory);
        command.Parameters.AddWithValue("@type", newType.ToString());
        command.ExecuteNonQuery();

    }


}