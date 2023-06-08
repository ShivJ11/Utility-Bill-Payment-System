using DAL.BuisnessServices.UserDetailsAPI.Contract;
using DAL.Models.Login;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.UserDetailsAPI.Implementation
{
    public class GetLoginStatus : IGetLoginStatus
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<LoginOutput> LoginStatusAsync(LoginInput LoginInput)
        {
            LoginOutput output = new LoginOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "utility_login";
                _command.Parameters.AddWithValue("@Email", LoginInput.Email);
                _command.Parameters.AddWithValue("@Password", LoginInput.Password);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    output.status = Convert.ToInt32(dr["status"]);
                    if (!dr.IsDBNull("userId"))
                    {
                        output.userID = Convert.ToInt32(dr["userId"]);
                        output.uFirstName = dr["UFirstName"].ToString();
                        output.uLastName = dr["ULastName"].ToString();
                        output.uEmail = dr["UEmail"].ToString();
                        output.uContactNumber = dr["uContactNumber"].ToString();
                    }
                }
                _connection.Close();
            }
            return output;
        }

    }
}
