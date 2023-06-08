namespace CustomerPortal.Models
{
    public class AccountDetails
    {
        public int AccountID { get; set; }
        public string AccountNumber { get; set; }
        public string Zip { get; set; }
        public string DueDate { get; set; }
        public decimal DueAmount { get; set; }
        public string AccountType { get; set; }
        public bool AccountStatus { get; set; }
        public int userID { get; set; }
    }
}
