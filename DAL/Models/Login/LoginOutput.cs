using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Login
{
    public class LoginOutput
    {
        public int status { get; set; }
        public int? userID { get; set; }
        public string? uFirstName { get; set; }
        public string? uLastName { get; set; }
        public string? uEmail { get; set; }

        public string? uContactNumber { get; set; }
    }
}
