namespace CustomerPortal.Models
{
    public class TransactionInput
    {

        public string PaymentDate { get; set; }
        public int CardID { get; set; }
        public double TransAmount { get; set; }
        public int AccountID { get; set; }
        public int userID { get; set; }
    }
}
