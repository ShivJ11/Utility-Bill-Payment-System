using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.Controllers
{
    public class FAQController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public FAQController(IConfiguration _config)
        {
            config = _config;
            apiGateway = new UserApiGateway(config);
        }
        public IActionResult Index(string token)
        {
            UserDetails user = apiGateway.GetUserFromToken(token);

            ViewBag.userID = user.userID;
            ViewBag.username = user.uFirstName;
            ViewBag.token = token;

            return View();
        }
    }
}
