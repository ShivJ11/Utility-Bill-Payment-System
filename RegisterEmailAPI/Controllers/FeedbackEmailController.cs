using DAL.BuisnessServices.EmailAPI.Contract;
using DAL.Models.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using RegisterEmailAPI.Models;

namespace RegisterEmailAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedbackEmailController : Controller
	{
		private readonly IContactUs Contact;
		public FeedbackEmailController(IContactUs _Contact)
		{
			Contact = _Contact;
		}
		[HttpPost]
		public async Task<ActionResult<ContactUsOutput>> ContactUsEmail(ContactUsInput details)
		{
			return await Contact.ContactUsEmail(details);			
		}
	}
}
