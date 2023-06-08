using DAL.BuisnessServices.AccountsAPI.Contract;
using DAL.Models.Accounts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.AccountsAPI.Implementation
{
    public class UpdateAccountStatus : IUpdateAccountStatus
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public async Task<AccountsOutput> UpdateAccount(int accID)
        {
            AccountsOutput output = new AccountsOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "update_account_status";
                _command.Parameters.AddWithValue("@AccountID", accID);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    output.Status = Convert.ToInt32(dr["status"]);
                }
                _connection.Close();
            }
            return output;
        }
    }
}
