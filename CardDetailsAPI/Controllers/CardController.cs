using DAL.BuisnessServices.CardsAPI.Contract;
using DAL.Models.Cards;
using DAL.Models.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IAddCard _addCard;
        private readonly IGetCardDetails _getCardDetails;
        
        public CardController(IAddCard addCard, IGetCardDetails getCardDetails)
        {
            _addCard = addCard;
            _getCardDetails = getCardDetails;
        }
        [HttpPost]
        public async Task<ActionResult<CardsOutput>> AddCard(CardDetails input)
        {
            return await _addCard.AddCards(input);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<CardDetails>>> GetCardDetails([FromQuery]int id)
        {
            return await _getCardDetails.CardsDetails(id);
        }
        /*[HttpGet]
        public async Task<ActionResult<CardDetails>> GetCardDetailsbyCardID([FromQuery] int CardID)
        {
            return await _getCardDetailsByCardID.GetCardDetailByCardID(CardID);
        }*/
    }
}
