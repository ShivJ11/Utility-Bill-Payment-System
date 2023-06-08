using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace CustomerPortal.Controllers
{
	public class ContactController : Controller
	{
		private IConfiguration config;
		UserApiGateway apiGateway;
		public ContactController(IConfiguration _config)
        {
			config = _config;
			apiGateway = new UserApiGateway(config);
		}

        public IActionResult Index(string token)
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
		public async Task<IActionResult> SendFeedbackEmail(ContactUsDetails details,string token)
		{
/*			contactUs.ContactUsEmail(userID,details.Email,details.Subject,details.Message);
*/			try
			{
                UserDetails user = apiGateway.GetUserFromToken(token);
                await apiGateway.UserFeedbackEmail(new ContactUsDetails
				{
					Email= details.Email,
					Subject= details.Subject,
					Message= details.Message,
					userID=user.userID
				});
				return RedirectToAction("Dashboard", "UserDashboard", new { token});
			}
			catch (Exception ex)
			{
				return View("~/Views/Shared/Error.cshtml");
			}
		}
	}
}
