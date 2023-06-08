using CustomerPortal.Common_code;
using CustomerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPortal.Controllers
{
    public class RegisterController : Controller
    {
        private IConfiguration config;
        UserApiGateway apiGateway;
        public RegisterController(IConfiguration _config)
        {
            config = _config;
            apiGateway = new UserApiGateway(config);
        }
        
        public IActionResult Index(int? errorId)
        {
            try
            {
                if (errorId > 0)
                {
                    ViewData["Message"] = "Email ID is already registered!!";
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            
        }
        public async Task<IActionResult> UserRegister(UserDetails userDetails)
        {
            RegisterResponse user = await apiGateway.RegisterUser(userDetails);
            if (user.status == 1)
            {
                await apiGateway.RegisterUserWelcomeEmail(new EmailRequestModel
                {
                    Email = userDetails.uEmail,
                    Name = userDetails.uFirstName + " " + userDetails.uLastName
                });
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index", new { @errorId = 1 });
            }
            /*try
            {
                
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }*/
            
        }
    }
}
