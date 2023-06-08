using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.Controllers
{
    public class UserDashboardController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public UserDashboardController(IConfiguration _config)
        {
            config = _config;
            apiGateway = new UserApiGateway(config);
        }
        [ActionName("Dashboard")]
        public async Task<IActionResult> Dashboard(string token)
        {
            try
            {
                UserDetails user=apiGateway.GetUserFromToken(token);
                var details = await apiGateway.GetTransactionDetails(user.userID);
                var accounts = await apiGateway.GetAccountDetails(user.userID);
                double countPendingBills = 0;
                double countPaidBills = 0;
                AccountDetails accountDetails = new AccountDetails();
                foreach (var item in accounts)
                {
                    if (item.AccountStatus == false)
                    {
                        countPendingBills++;
                        accountDetails = item;
                    }
                    else
                    {
                        countPaidBills++;
                    }
                }
                ViewData["data"]=accountDetails;
                List<TransactionDetails> trans = new List<TransactionDetails>();
                trans = await apiGateway.DisplayDetails(details);
                ViewBag.userID = user.userID;
                ViewBag.username = user.uFirstName;
                ViewBag.token = token;
                ViewBag.pendingBills=countPendingBills;
                double totalBills = countPaidBills + countPendingBills;
                if (countPaidBills == 0)
                {
                    ViewBag.percBills="No Bills Reamining";
                }
                else
                {
                    ViewBag.percBills = Math.Round((countPaidBills / totalBills)*100,2);
                }
                return View(trans);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
