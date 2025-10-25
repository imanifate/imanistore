using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Aplication.Utilities
{
    public static class MessageSender
    {
        public static void SMS(string to, string body)
        {
            //var api = new KavenegarApi("");
            //api.Send("10004346", to, body);
        }

        public static void Email(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("imanifateimani1984@gmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;

            SmtpServer.Credentials = new System.Net.NetworkCredential("imanifateimani1984@gmail.com", "vkjluftkskcsbvpg");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
