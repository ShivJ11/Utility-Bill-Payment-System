using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using SelectPdf;

namespace CustomerPortal.Controllers
{
    public class PaidBillsController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public PaidBillsController(IConfiguration _config)
        {
            config = _config;
            apiGateway = new UserApiGateway(config);
        }
        public async Task<IActionResult> Index(string token)
        {

            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                var accounts = await apiGateway.GetAccountDetails(user.userID);
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.token = token;

                return View(accounts);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        [ActionName("Reciept")]
        public async Task<IActionResult> Reciept(string token, int accID)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                var cards = await apiGateway.GetCardDetails(user.userID);
                var transactions = await apiGateway.GetTransactionDetails(user.userID);
                var matchedTrans = transactions.FirstOrDefault(trans => trans.AccountID == accID);
                var matchedCard = cards.FirstOrDefault(card => card.CardID == matchedTrans.CardID);
                var accounts = await apiGateway.GetAccountDetails(user.userID);
                var matchedAcc = accounts.FirstOrDefault(acc => acc.AccountID == accID);

                var details = await apiGateway.GetTransactionDetails(user.userID);
                
                ViewBag.cardNumber = matchedCard.CardNumber;
                ViewBag.cardType = matchedCard.CardType;
                ViewBag.accType = matchedAcc.AccountType;
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.username = user.uFirstName;
                ViewBag.email = user.uEmail;
                ViewBag.contact = user.uContactNumber;
                ViewBag.token = token;

                ViewData["data"]=matchedTrans;
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

    }
}
    
