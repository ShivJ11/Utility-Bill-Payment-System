using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace CustomerPortal.Controllers
{
    public class TransactionController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public TransactionController(IConfiguration _config)
        {
            config = _config;
            apiGateway = new UserApiGateway(config);
        }
        public async Task<IActionResult> Index(string token)
        {
            try
            {
                UserDetails user = apiGateway.GetUserFromToken(token);
                var details = await apiGateway.GetTransactionDetails(user.userID);
                List<TransactionDetails> trans=new List<TransactionDetails>();
                trans = await apiGateway.DisplayDetails(details); 
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.token = token;

                return View(trans);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
