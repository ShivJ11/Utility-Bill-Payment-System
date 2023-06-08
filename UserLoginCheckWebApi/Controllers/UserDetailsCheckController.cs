using DAL.BuisnessServices.UserDetailsAPI.Implementation;
using DAL.BuisnessServices.UserDetailsAPI.Contract;
using DAL.Models.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using NuGet.Protocol.Plugins;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsCheckController : ControllerBase
    {
        private readonly IGetLoginStatus _status;
        /*private readonly IGetLoginStatus _status;*/
        private readonly IConfiguration _config;
        public UserDetailsCheckController(IGetLoginStatus status, IConfiguration config)
        {
            _status = status;
            _config = config;
        }
        
        // GET: api/<UserDetailsCheckController>
        [HttpGet]
        public async Task<IActionResult> GetOutput([FromQuery]LoginInput input)
        {
             
            IActionResult response = Unauthorized();
            LoginOutput user = await _status.LoginStatusAsync(input);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(LoginOutput userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            if( userInfo.status!=0)
            {
                claims.Add(new Claim("status", "1"));
                claims.Add(new Claim("userId", userInfo.userID.ToString())); // Example custom claim for user ID
                claims.Add(new Claim("username", userInfo.uFirstName + userInfo.uLastName));
                claims.Add(new Claim("email", userInfo.uEmail));
                claims.Add(new Claim("contact", userInfo.uContactNumber));
                // Example custom claim for username
    // Add more custom claims as needed
            }
            if (userInfo.status == 0)
            {
                claims.Add(new Claim("status", "0"));
            }
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    
}
