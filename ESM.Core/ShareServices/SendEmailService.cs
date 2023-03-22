using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Core.ShareServices
{
    public interface ISendEmailService
    {
        public Task<bool> BeginSendEmail(string email, string message, bool isBodyHtml = false);
    }
    public class SendEmailService : ISendEmailService
    {

        public SendEmailService()
        {

        }

        public async Task<bool> BeginSendEmail(string toEmail, string message, bool isBodyHtml = false)
        {
            string from = "inbaicualong@gmail.com";
            string pass = "ccxwnfnfgoysqreu";
            MailMessage mess = new()
            {
                Subject = "Reset Password",
                From = new MailAddress(from)
            };
            mess.To.Add(toEmail);
            mess.Body = message;
            mess.IsBodyHtml = isBodyHtml;
            SmtpClient client = new("smtp.gmail.com")
            {
                EnableSsl = true,
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(from, pass)
            };
            Task<bool> task = new(() => Send(client, mess));
            task.Start();
            await task;
            return task.Result;
        }
        private bool Send(SmtpClient client, MailMessage mess)
        {
            try
            {
                client.Send(mess);
            }
            catch (Exception) { return false; }
            return true;
        }

    }
}
