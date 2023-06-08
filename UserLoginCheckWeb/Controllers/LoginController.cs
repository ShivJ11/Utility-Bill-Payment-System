using Microsoft.AspNetCore.Mvc;
using CustomerPortal.Models;
using CustomerPortal.Common_code;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CustomerPortal.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public LoginController(IConfiguration _config)
        {
            config = _config;
            apiGateway = new UserApiGateway(config);
        }
        public IActionResult Index(int? errorId)
        {
            try
            {
                if (errorId == 1)
                {
                    ViewData["Message"] = "Incorrect Username or Password !!";
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            
        }
        public async Task<IActionResult> Check(UserDetails userdetails)
        {
            try
            {
                string token = await apiGateway.LoginCheck(userdetails.uEmail, userdetails.uPassword);
                UserDetails user=apiGateway.GetUserFromToken(token);
                if (user.status == 1)
                {
                    string username = user.uFirstName;
                    return RedirectToAction("Dashboard", "UserDashboard", new {token });
                }
                else
                {
                    return RedirectToAction("Index", new { @errorId = 1 });
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        public IActionResult Logout()
        {
            try
            {
                // Clear the authentication cookies
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Disable browser caching
                Response.Headers["Cache-Control"] = "no-cache, no-store";
                Response.Headers["Pragma"] = "no-cache";
                Response.Headers["Expires"] = "-1";

                // Redirect to the login page
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
