using DAL.BuisnessServices.TransactionAPI.Contract;
using DAL.Models.Transaction;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.TransactionAPI.Implementation
{
    public class AddTransaction : IAddTransaction
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<TransactionOutput> AddTransAction(TransactionDetails transactionDetails)
        {
            TransactionOutput output = new TransactionOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "insert_trans";
                _command.Parameters.AddWithValue("@PaymentDate", transactionDetails.PaymentDate);
                _command.Parameters.AddWithValue("@CardID",transactionDetails.CardID);
                _command.Parameters.AddWithValue("@TransAmount",transactionDetails.TransAmount);
                _command.Parameters.AddWithValue("@AccountID",transactionDetails.AccountID);
                _command.Parameters.AddWithValue("@userID",transactionDetails.userID);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    output.status = Convert.ToInt32(dr["status"]);
                    output.message = dr["message"].ToString();
                }
                _connection.Close();
            }
            return output;
        }
    }
}
