using DAL.BuisnessServices.CardsAPI.Contract;
using DAL.Models.Cards;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.CardsAPI.Implementation
{
    public class GetCardDetailsByCardID : IGetCardDetailByCardID
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public async Task<CardDetails> GetCardDetailByCardID(int CardID)
        {
            CardDetails card=new CardDetails();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "get_card_details_by_cardID";
                _command.Parameters.AddWithValue("@CardID", CardID);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr.IsDBNull("userID"))
                    {
                        break;
                    }
                    card.CardType = dr["CardType"].ToString();
                    card.CardNumber = Convert.ToInt32(dr["CardNumber"]);
                    card.CardExpMonth = Convert.ToInt32(dr["CardExpMonth"]);
                    card.CardExpYear = Convert.ToInt32(dr["CardExpYear"]);
                }
                _connection.Close();
            }
            return card;
        }
    }
}
