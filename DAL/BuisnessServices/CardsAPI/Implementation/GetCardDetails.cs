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
    public class GetCardDetails : IGetCardDetails
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<List<CardDetails>> CardsDetails(int userID)
        {
            List<CardDetails> cards = new List<CardDetails>();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "get_card_details";
                _command.Parameters.AddWithValue("@userID", userID);                
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if(dr.IsDBNull("userID"))
                    {
                        break;
                    }
                    CardDetails card = new CardDetails();
                    card.CardType = dr["CardType"].ToString();
                    card.CardID = Convert.ToInt32(dr["CardID"]);
                    card.CardNumber = Convert.ToInt32(dr["CardNumber"]);
                    card.CardExpMonth = Convert.ToInt32(dr["CardExpMonth"]);
                    card.CardExpYear = Convert.ToInt32(dr["CardExpYear"]);
                    card.userID = userID;
                    cards.Add(card);
                }
                _connection.Close();
            }
            return cards;
        }
    }
}
