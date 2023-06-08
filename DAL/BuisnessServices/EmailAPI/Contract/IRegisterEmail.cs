using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.EmailAPI.Contract
{
    public interface IRegisterEmail
    {
        public void RegisterUserWelcomeEmail(string email,string name);
    }
}
