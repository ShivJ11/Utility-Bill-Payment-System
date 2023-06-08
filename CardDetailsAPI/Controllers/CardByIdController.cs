using DAL.BuisnessServices.CardsAPI.Contract;
using DAL.Models.Cards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardByIdController : ControllerBase
    {
        private readonly IGetCardDetailByCardID _getCardDetailsByCardID;
        public CardByIdController(IGetCardDetailByCardID getCardDetailsByCardID)
        {
            _getCardDetailsByCardID = getCardDetailsByCardID;
        }
        [HttpGet]
        public async Task<ActionResult<CardDetails>> GetCardDetailsbyCardID([FromQuery] int CardID)
        {
            return await _getCardDetailsByCardID.GetCardDetailByCardID(CardID);
        }
    }
}
