using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.Controllers
{
    public class UserCardsController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public UserCardsController(IConfiguration _config)
        {
            config = _config;
            apiGateway = new UserApiGateway(config);
        }

        public IActionResult Index(string token)
        {
            try
            {
                UserDetails user=apiGateway.GetUserFromToken(token);
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.token = token;

                return View();
            }
            catch(Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            
        }
        
        public async Task<IActionResult> AddCards(CardDetails carddetails, string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                if (carddetails.CardType == "Nothing")
                {
                    TempData["Error"] = "No card exists with such card number";
                    return RedirectToAction("Index", new {  token });
                }
                carddetails.CardNumber = carddetails.CardNumber % 10000;
                carddetails.userID = user.userID;
                CardResponse cardresp = await apiGateway.AddCardDetails(carddetails);
                if (cardresp.status == 1)
                {
                    return RedirectToAction("MyCards", "UserCards", new { token });
                }
                else
                {
                    return RedirectToAction("MyCards", "UserCards", new { token});

                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            
        }
        [ActionName("MyCards")]
        public async Task<IActionResult> MycardsAsync(string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);

                var cards =  await apiGateway.GetCardDetails(user.userID);
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.token = token;
                return View(cards);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            
        }

    }
}
