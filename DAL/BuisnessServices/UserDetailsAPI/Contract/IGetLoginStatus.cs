using DAL.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.UserDetailsAPI.Contract
{
    public interface IGetLoginStatus
    {
        Task<LoginOutput> LoginStatusAsync(LoginInput LoginInput);
    }
}
