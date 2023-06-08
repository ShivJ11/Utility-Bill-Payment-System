using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Cards
{
    public class CardDetails
    {
        public int CardID { get; set; }
        public string CardType { get; set; }
        public int CardNumber { get; set; }
        public int CardExpMonth { get; set; }
        public int CardExpYear { get; set; }
        public int userID { get; set; }
    }
}
