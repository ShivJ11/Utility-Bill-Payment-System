using DAL.BuisnessServices.AccountsAPI.Contract;
using DAL.Models.Accounts;
using DAL.Models.Cards;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.AccountsAPI.Implementation
{
    public class GetAccountDetails : IGetAccountDetails
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<List<AccountDetails>> GetAccounts(int userID)
        {
            List<AccountDetails> accounts = new List<AccountDetails>();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "get_accounts";
                _command.Parameters.AddWithValue("@userID", userID);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr.IsDBNull("userID"))
                    {
                        break;
                    }
                    AccountDetails acc = new AccountDetails();
                    acc.userID = Convert.ToInt32(dr["userID"]);                    
                    acc.AccountNumber = dr["AccountNumber"].ToString();
                    acc.AccountID = Convert.ToInt32(dr["AccountID"]);
                    acc.Zip = dr["Zip"].ToString();
                    acc.DueDate = dr["DueDate"].ToString();
                    acc.DueAmount = Convert.ToDecimal(dr["DueAmount"]);
                    acc.AccountType = dr["AccountType"].ToString();
                    acc.AccountStatus = false;
                    int accStatus = Convert.ToInt32(dr["AccountStatus"]);
                    if (accStatus == 1) acc.AccountStatus = true;
                    accounts.Add(acc);
                }
                _connection.Close();
            }
            return accounts;
        }
    }
}
