using DAL.BuisnessServices.AccountsAPI.Contract;
using DAL.Models.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateAccountController : ControllerBase
    {
        private readonly IUpdateAccountStatus _updateAccountStatus;
        public UpdateAccountController(IUpdateAccountStatus updateAccountStatus)
        {
            _updateAccountStatus = updateAccountStatus;
        }
        [HttpGet]
        public async Task<ActionResult<AccountsOutput>> UpdateAccount(int AccId)
        {
            return await _updateAccountStatus.UpdateAccount(AccId);
        }
    }
}
