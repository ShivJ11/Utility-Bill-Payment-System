using DAL.BuisnessServices.UserDetailsAPI.Contract;
using DAL.Models.Login;
using DAL.Models.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace UserDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IRegisterUser _register;
        public RegisterUserController(IRegisterUser register)
        {
            _register = register;
        }
        [HttpPost]
        public async Task<ActionResult<RegisterOutput>> GetInput(UserDetails input)
        {
            return await _register.RegisterUsers(input);
        }
    }
}
