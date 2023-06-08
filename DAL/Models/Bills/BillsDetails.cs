using DAL.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Bills
{
    public class BillsDetails
    {
        public int AccountID { get; set; }    
        public string BillGenerationDate { get; set; }
        public double ServiceFee { get;set; }
        public string BillDueDate { get; set; }
        public double BillAmount { get; set; }
        public string BillType { get; set; }
        public string BillStatus { get; set; }
    }
}
