using DAL.BuisnessServices.EmailAPI.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.Email;
using Microsoft.Data.SqlClient;
using DAL.Models.Login;
using System.Data;

namespace DAL.BuisnessServices.EmailAPI.Implementation
{
    public class ContactUs : IContactUs
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public Task<ContactUsOutput> ContactUsEmail(ContactUsInput details)
        {
            try
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("");
                msg.To.Add(details.Email);
                msg.Subject = "Thanks for Contacting Us";
                msg.Body = "Thanks for contacting and sharing your valuable message with us." + "\n" + "Your message is:\n" + details.Message;
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

                return ContactUsSPCall(details);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -" + ex);
                ContactUsOutput message = new ContactUsOutput();
                message.Message = "Some Error Occured";
                return Task.FromResult(message);
            }
        }

        private async Task<ContactUsOutput> ContactUsSPCall(ContactUsInput details)
        {
            ContactUsOutput response = new ContactUsOutput();
            using (_connection = new SqlConnection(Common.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "user_feedback";
                _command.Parameters.AddWithValue("@userID", details.userID);
                _command.Parameters.AddWithValue("@Subject", details.Subject);
                _command.Parameters.AddWithValue("@Message", details.Message);
                _command.Parameters.AddWithValue("@Email", details.Email);
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    response.Status = Convert.ToInt32(dr["Status"]);
                    response.Message = dr["MESSAGE"].ToString();
                }
                _connection.Close();
            }
            return response;
        }
    }
}

