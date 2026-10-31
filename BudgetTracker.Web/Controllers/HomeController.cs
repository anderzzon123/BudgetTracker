using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Web.Models;
using BudgetTracker;

namespace BudgetTracker.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        TransactionRepository repository = new();

        var transactions = repository.GetAllTransactions();

        BudgetManager manager = new();

        foreach (Transaction transaction in transactions)
        {
            manager.AddTransaction(transaction);
        }

        ViewBag.Balance = manager.GetBalance();
        ViewBag.Transactions = transactions;

        return View();
    }
    [HttpPost]
    public IActionResult addTransaction(string category, decimal amount, TransactionType type)
    {

        if (amount <= 0)
        {
            TempData["Error"] = "Beloppet måste vara större än 0";
            return RedirectToAction("Index");
        }
        if (string.IsNullOrWhiteSpace(category))
        {
            TempData["Error"] = "Du måste ange en kategori";
            return RedirectToAction("Index");
        }

        Transaction transaction = new Transaction(0, type, amount, category);

        TransactionRepository repository = new();

        repository.SaveTransaction(transaction);

        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult DeleteTransaction(int id)
    {
        TransactionRepository repository = new();
        repository.DeleteTransaction(id);

        return RedirectToAction("Index");
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
