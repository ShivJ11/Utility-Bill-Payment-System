using DAL.BuisnessServices.BillsAPI.Contract;
using DAL.Models.Bills;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IAddBill _addBill;
        private readonly IGetBillDetails _getBillDetails;
        public BillController(IAddBill addBill,IGetBillDetails getBillDetails)
        {
            _addBill = addBill;
            _getBillDetails = getBillDetails;
        }
        [HttpPost]
        public async Task<ActionResult<BillsOutput>> addBill(BillsDetails details)
        {
            return await _addBill.AddBills(details);
        }
        [HttpGet]
        public async Task<ActionResult<List<BillsDetails>>> GetBillDetails([FromQuery] int id)
        {
            return await _getBillDetails.BillDetails(id);
        }
    }
}
