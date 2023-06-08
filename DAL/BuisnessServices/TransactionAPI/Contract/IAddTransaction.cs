using DAL.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.TransactionAPI.Contract
{
    public interface IAddTransaction
    {
        public Task<TransactionOutput> AddTransAction(TransactionDetails transactionDetails);
    }
}
