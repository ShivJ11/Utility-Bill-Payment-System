using DAL.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.EmailAPI.Contract
{
    public interface IContactUs
    {
        public Task<ContactUsOutput> ContactUsEmail(ContactUsInput details);
    }
}
