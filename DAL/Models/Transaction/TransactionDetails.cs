using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Transaction
{
    public class TransactionDetails
    {
        public int TransID { get; set; }
        public string PaymentDate{ get; set; }
        public int CardID { get; set; }
        public double TransAmount { get; set; }
        public int AccountID { get;set; }
        public int userID { get; set; }
    }
}
