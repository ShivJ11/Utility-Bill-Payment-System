using DAL.Models.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.BillsAPI.Contract
{
    public interface IGetBillDetails
    {
        public Task<List<BillsDetails>> BillDetails(int AccountID);
    }
}
