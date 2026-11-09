using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Web.Models;
using BudgetTracker;

namespace BudgetTracker.Web.Controllers;

public class HomeController : Controller
{

    private readonly TransactionService service;

    public HomeController(TransactionService service)
    {
        this.service = service;
    }

    public IActionResult Index()
    {
        var transactions = service.GetAllTransactions();


        ViewBag.Transactions = transactions;
        ViewBag.Balance = service.GetBalance();

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

        service.AddTransaction(transaction);

        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult DeleteTransaction(int id)
    {
        service.DeleteTransaction(id);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateTransaction(int id, decimal newAmount, string newCategory, TransactionType newType)
    {
        try
        {
            service.UpdateTransaction(id, newAmount, newCategory, newType);

        }
        catch (ArgumentException ex)
        {
            TempData["Error"] = ex.Message;
        }
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
