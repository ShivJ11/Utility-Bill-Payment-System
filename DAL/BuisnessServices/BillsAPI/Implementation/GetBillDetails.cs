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
    public class GetBillDetails : IGetBillDetails
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<List<BillsDetails>> BillDetails(int AccountID)
        {
            List<BillsDetails> bills = new List<BillsDetails>();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "get_bill_details";
                _command.Parameters.AddWithValue("@AccountID", AccountID);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr.IsDBNull("AccountID"))
                    {
                        break;
                    }
                    BillsDetails bill=new BillsDetails();
                    bill.AccountID = AccountID;
                    bill.BillGenerationDate = dr["BillGenerationDate"].ToString();
                    bill.ServiceFee = Convert.ToDouble(dr["ServiceFee"]);
                    bill.BillDueDate= dr["BillDueDate"].ToString();
                    bill.BillAmount = Convert.ToDouble(dr["BillAmount"]);
                    bill.BillType= dr["BillType"].ToString();
                    bill.BillStatus = dr["BillStatus"].ToString();
                    bills.Add(bill);
                }
                _connection.Close();
            }
            return bills;
        }
    }
}
