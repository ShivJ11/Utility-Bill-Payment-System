using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Cards
{
    public class CardsOutput
    {
        public int status { get; set; }
        public string message { get; set; }
    }
}
