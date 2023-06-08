using DAL.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.CardsAPI.Contract
{
    public interface IGetCardDetailByCardID
    {
        public Task<CardDetails> GetCardDetailByCardID(int CardID);
    }
}
