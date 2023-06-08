using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.Controllers
{
    public class PendingBillsController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public PendingBillsController(IConfiguration _config)
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
    }
}
