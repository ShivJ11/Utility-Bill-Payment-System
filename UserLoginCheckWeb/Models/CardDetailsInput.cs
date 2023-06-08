namespace CustomerPortal.Models
{
    public class CardDetailsInput
    {
        public long CardNumber { get; set; }
        public int DefaultCard
        {
            get ; set ;
           /*get { return DefaultCard; }
            set
            {
                if (DefaultCard == "true")
                {
                    DefaultCard = "1";
                }
                DefaultCard = "0";
            }*/
        }
        public int? userID { get; set; }
    }

}
