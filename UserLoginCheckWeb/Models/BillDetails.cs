namespace CustomerPortal.Models
{
    public class BillDetails
    {
        public int AccountID { get; set; }
        public string BillGenerationDate { get; set; }
        public double ServiceFee { get; set; }
        public string BillDueDate { get; set; }
        public double BillAmount { get; set; }
        public string BillType { get; set; }
        public string BillStatus { get; set; } = string.Empty;
        public bool isSelected { get; set; } = false;
    }
}
