using DAL.BuisnessServices.UserDetailsAPI.Contract;
using DAL.Models.Login;
using DAL.Models.Register;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.UserDetailsAPI.Implementation
{
    public class RegisterUser : IRegisterUser
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<RegisterOutput> RegisterUsers(UserDetails input)
        {
            RegisterOutput output = new RegisterOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "utility_register";
                _command.Parameters.AddWithValue("@uFirstName", input.uFirstName);
                _command.Parameters.AddWithValue("@uLastName", input.uLastName);
                _command.Parameters.AddWithValue("@uEmail", input.uEmail);
                _command.Parameters.AddWithValue("@uPassword", input.uPassword);
                _command.Parameters.AddWithValue("@uContactNumber", input.uContactNumber);
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
