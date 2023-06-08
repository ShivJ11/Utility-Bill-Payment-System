using DAL.Models.Login;
using DAL.Models.Register;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.UserDetailsAPI.Contract
{
    public interface IRegisterUser
    {
        Task<RegisterOutput> RegisterUsers(UserDetails input);
    }
}
