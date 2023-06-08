using DAL.BuisnessServices.EmailAPI.Contract;
using DAL.BuisnessServices.EmailAPI.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterEmailAPI.Common;
using RegisterEmailAPI.Models;

namespace RegisterEmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterEmailController : ControllerBase
    {
        private readonly IRegisterEmail Register;


        public RegisterEmailController(IRegisterEmail _Register)
        {
            Register = _Register;
        }

        
        [HttpPost]
        public async Task<IActionResult> SendRegisterEmail(RegisterEmailModel email)
        {
            bool isValid = EmailValidator.IsValidEmailAddress(email.Email);
            if (!isValid)
            {
                return BadRequest("Invalid Email");
            }
            Register.RegisterUserWelcomeEmail(email.Email, email.Name);
            return Ok();
        }
    }
}
