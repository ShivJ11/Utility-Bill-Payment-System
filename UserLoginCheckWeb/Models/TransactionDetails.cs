namespace CustomerPortal.Models
{
    public class TransactionDetails
    {
        public int TransID { get; set; }
        public string PaymentDate { get; set; }
        public int CardID { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public double TransAmount { get; set; }
        public int AccountID { get; set; }
        public string AccountNumber { get; set; }
        public int userID { get; set; }
    }
}
