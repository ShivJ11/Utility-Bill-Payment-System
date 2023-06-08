namespace CustomerPortal.Models
{
    public class UserDetails
    {
        public int userID { get;set; }
        public string uFirstName { get; set; }
        public string? uLastName { get; set; }
        public string? uEmail { get; set; }
        public string? uPassword { get; set; }
        public string? uContactNumber { get; set; }
        public int status { get; set; }

    }
}
