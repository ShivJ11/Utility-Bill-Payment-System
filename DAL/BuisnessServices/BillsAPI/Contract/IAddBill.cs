using DAL.Models.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.BillsAPI.Contract
{
    public interface IAddBill
    {
        Task<BillsOutput> AddBills(BillsDetails billsDetails);
    }
}
