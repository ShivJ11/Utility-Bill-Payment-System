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
    public class GetTransactionDetails : IGetTransactionDetails
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<List<TransactionDetails>> TransactionDetails(int userID)
        {
            List<TransactionDetails> transactions = new List<TransactionDetails>();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "get_trans";
                _command.Parameters.AddWithValue("@userID", userID);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr.IsDBNull("TransID"))
                    {
                        break;
                    }
                    TransactionDetails transaction=new TransactionDetails();
                    transaction.TransID = Convert.ToInt32(dr["TransID"]);
                    transaction.PaymentDate = dr["PaymentDate"].ToString();
                    transaction.CardID = Convert.ToInt32(dr["CardID"]);
                    transaction.TransAmount = Convert.ToDouble(dr["TransAmount"]);
                    transaction.AccountID = Convert.ToInt32(dr["AccountID"]);
                    transaction.userID = userID;
                    transactions.Add(transaction);
                }
                _connection.Close();
            }
            return transactions;
        }
    }
}
