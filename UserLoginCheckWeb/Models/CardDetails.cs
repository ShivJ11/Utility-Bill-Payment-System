using Microsoft.CodeAnalysis.Options;

namespace CustomerPortal.Models
{
    public class CardDetails
    {
        public int CardID { get; set; }
        public string CardType { get; set; }
        public long CardNumber { get; set; }
        public int CardExpMonth { get; set; }
        public int CardExpYear { get; set; }
        public int? userID { get; set; }
        public string Path
        {
            get
            {
                if(CardType == "Visa")
                {
                    return "~/img/Cards/visa.svg";
                }
                else if (CardType == "MasterCard") 
                {
                    return "~/img/Cards/mastercard.svg";
                }
                else if (CardType == "AmericanExpress")
                {
                    return "~/img/Cards/amex.svg";
                }
                else if (CardType == "Diners")
                {
                    return "~/img/Cards/diners.svg";
                }
                else if (CardType == "Discover")
                {
                    return "~/img/Cards/discover.svg";
                }
                else if (CardType == "JCB")
                {
                    return "~/img/Cards/jcb.svg";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
