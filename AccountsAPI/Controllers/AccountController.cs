using DAL.BuisnessServices.AccountsAPI.Contract;
using DAL.Models.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace AccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAddAccount _addAcc;
        private readonly IGetAccountDetails _getDetails;
        
        public AccountController(IAddAccount addAcc, IGetAccountDetails getDetails)
        {
            _addAcc = addAcc;
            _getDetails = getDetails;
            

        }

        [HttpGet]
        public async Task<ActionResult<List<AccountDetails>>> GetCardDetails([FromQuery] int id)
        {
            return await _getDetails.GetAccounts(id);
        }

        [HttpPost]
        public async Task<ActionResult<AccountsOutput>> AddAccount(AccountDetails accDetails)
        {
            return await _addAcc.AddAccounts(accDetails);
        }
    }
}
