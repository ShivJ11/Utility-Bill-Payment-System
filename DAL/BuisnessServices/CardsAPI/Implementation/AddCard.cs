using DAL.BuisnessServices.CardsAPI.Contract;
using DAL.Models.Cards;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.CardsAPI.Implementation
{
    public class AddCard : IAddCard
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<CardsOutput> AddCards(CardDetails input)
        {
            CardsOutput output = new CardsOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "card_details";
                _command.Parameters.AddWithValue("@CardType", input.CardType);
                _command.Parameters.AddWithValue("@CardNumber", input.CardNumber);
                _command.Parameters.AddWithValue("@CardExpMonth", input.CardExpMonth);
                _command.Parameters.AddWithValue("@CardExpYear", input.CardExpYear);
                _command.Parameters.AddWithValue("@userID", input.userID);
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
