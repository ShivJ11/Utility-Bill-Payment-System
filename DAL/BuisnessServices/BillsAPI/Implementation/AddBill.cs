using DAL.BuisnessServices.BillsAPI.Contract;
using DAL.Models.Bills;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.BillsAPI.Implementation
{
    public class AddBill : IAddBill
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<BillsOutput> AddBills(BillsDetails billsDetails)
        {
            BillsOutput output = new BillsOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "add_bill";
                _command.Parameters.AddWithValue("@AccountID", billsDetails.AccountID);
                _command.Parameters.AddWithValue("@BillGenerationDate", billsDetails.BillGenerationDate);
                _command.Parameters.AddWithValue("@ServiceFee", billsDetails.ServiceFee);
                _command.Parameters.AddWithValue("@BillDueDate", billsDetails.BillDueDate);
                _command.Parameters.AddWithValue("@BillAmount", billsDetails.BillAmount);
                _command.Parameters.AddWithValue("@BillType", billsDetails.BillType);
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
