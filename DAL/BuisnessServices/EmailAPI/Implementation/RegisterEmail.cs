using DAL.BuisnessServices.EmailAPI.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.EmailAPI.Implementation
{
    public class RegisterEmail : IRegisterEmail
    {
        public void RegisterUserWelcomeEmail(string email, string name)
        {
            try
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("");
                msg.To.Add(email);
                msg.Subject = "Welcome to utility Bill Payment System";
                msg.Body = "Hello "+name+",!! Welcome to Utility Bill Payment System";
                //msg.Priority = MailPriority.High;


                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("", "");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -" + ex);
            }
        }
    }
}
