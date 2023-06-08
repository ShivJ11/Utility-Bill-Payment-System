using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.Controllers
{
    public class AccountsController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public AccountsController(IConfiguration _config)
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
                ViewBag.token=token;
                return View(accounts);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public IActionResult AddAccount(string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.token = token;
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(AccountDetails model, string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                model.AccountStatus = false;
                model.userID = user.userID;
                AccountResponse response = await apiGateway.AddAccountDetails(model);
                if (response.Status == 1)
                {
                    return RedirectToAction("Index", new { token});
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during bill processing
                // For example, log the error, display an error message, etc.

                // Redirect to an error page or show an error message to the user
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PayBills(BillInfo billInfo,string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                var cards = await apiGateway.GetCardDetails(user.userID);
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.token = token;
                TempData["AccountID"] = billInfo.AccountID;
                TempData["DueAmount"] = billInfo.DueAmount;
                return View(cards);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Checkout(BillInfo billInfo,string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                var cards = await apiGateway.GetCardDetails(user.userID);
                var accounts = await apiGateway.GetAccountDetails(user.userID);
                var matchedAcc = accounts.FirstOrDefault(acc => acc.AccountID == billInfo.AccountID);
                var matchedCard = cards.FirstOrDefault(card => card.CardID == billInfo.CardID);
                TransactionInput details = new TransactionInput();
                details.AccountID = billInfo.AccountID;
                details.CardID = billInfo.CardID;
                details.TransAmount = Convert.ToDouble(billInfo.DueAmount);
                details.PaymentDate = DateTime.Now.ToString("dd-MMMM-yyyy");
                details.userID = user.userID;
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.email = user.uEmail;
                ViewBag.contact = user.uContactNumber;
                ViewBag.cardNumber = matchedCard.CardNumber;
                ViewBag.cardType = matchedCard.CardType;
                ViewBag.accType = matchedAcc.AccountType;
                ViewBag.token = token;

                return View(details);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Payment(TransactionInput input, string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                Response res = await apiGateway.AddTransactionDetails(input);
                if (res.Status == 1)
                {
                    ViewBag.userID = user.userID;
                    ViewBag.username = user.uFirstName;
                    ViewBag.token = token;
                    Response res2 = await apiGateway.UpdateAccountStatus(input.AccountID);
                    return View(new {token});
                }
                
                return View(new { token });
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
