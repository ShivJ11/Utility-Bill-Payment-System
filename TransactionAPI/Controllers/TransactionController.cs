using DAL.BuisnessServices.TransactionAPI.Contract;
using DAL.Models.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IAddTransaction _addTransaction;
        private readonly IGetTransactionDetails _getTransactionDetails;
        public TransactionController(IAddTransaction addTransaction,IGetTransactionDetails getTransactionDetails)
        {
            _addTransaction = addTransaction;
            _getTransactionDetails = getTransactionDetails;
        }
        [HttpGet]
        public async Task<ActionResult<List<TransactionDetails>>> GetTransDetails([FromQuery] int id)
        {
            return await _getTransactionDetails.TransactionDetails(id);
        }
        [HttpPost]
        public async Task<ActionResult<TransactionOutput>> AddTransaction(TransactionDetails details)
        {
            return await _addTransaction.AddTransAction(details);
        }
    }
}
