using DAL.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.AccountsAPI.Contract
{
    public interface IUpdateAccountStatus
    {
        public Task<AccountsOutput> UpdateAccount(int accID);
    }
}
