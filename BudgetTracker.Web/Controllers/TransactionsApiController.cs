using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Web.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsApiController : ControllerBase
{

    private readonly TransactionService service;

    public TransactionsApiController(TransactionService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAllTransactions()
    {
        List<Transaction> transactions = service.GetAllTransactions();

        return Ok(transactions);
    }
    [HttpGet("{id}")]
    public IActionResult GetTransactionById(int id)
    {
        Transaction? transaction = service.GetTransactionById(id);
        if (transaction != null)
        {
            return Ok(transaction);
        }
        else
        {
            return NotFound();
        }
    }

}