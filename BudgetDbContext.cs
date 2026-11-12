using Microsoft.EntityFrameworkCore;

namespace BudgetTracker;

public class BudgetDbContext : DbContext
{


    public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
    : base(options)
    {
    }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Transaction>()
        .Property(t => t.Amount)
        .HasConversion(
        amount => decimal.ToInt64(amount * 100),
        amountInOre => amountInOre / 100m);

        modelBuilder.Entity<Transaction>()
        .Property(t => t.Type)
        .HasConversion<string>();

        modelBuilder.Entity<Transaction>()
        .Property(t => t.Date).HasConversion(
        date => date.ToString("yyyy-MM-dd HH:mm:ss"),
        dateString => DateTime.Parse(dateString));

        
    }

}