using DAL.BuisnessServices.AccountsAPI.Contract;
using DAL.Models.Accounts;
using DAL.Models.Bills;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.AccountsAPI.Implementation
{
    public class AddAccount : IAddAccount
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public async Task<AccountsOutput> AddAccounts(AccountDetails accDetails)
        {
            AccountsOutput output = new AccountsOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "add_account";
                _command.Parameters.AddWithValue("@AccountNumber", accDetails.AccountNumber);
                _command.Parameters.AddWithValue("@Zip", accDetails.Zip);
                _command.Parameters.AddWithValue("@DueDate", accDetails.DueDate);
                _command.Parameters.AddWithValue("@DueAmount", accDetails.DueAmount);
                _command.Parameters.AddWithValue("@AccountType", accDetails.AccountType);
                int accStatus = 0;
                _command.Parameters.AddWithValue("@AccountStatus", accStatus);
                _command.Parameters.AddWithValue("@userID", accDetails.userID);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    output.Status = Convert.ToInt32(dr["status"]);
                    output.Message = dr["message"].ToString();
                }
                _connection.Close();
            }
            return output;
        }
    }
}
