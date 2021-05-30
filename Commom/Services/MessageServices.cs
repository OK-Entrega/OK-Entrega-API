using System.Net.Mail;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Commom.Services
{
    public static class MessageServices
    {
        public static void SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("noreply.okentrega@gmail.com")
            };

            mail.To.Add(to);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("noreply.okentrega@gmail.com", "0k3ntr3g4")
            };

            smtp.Send(mail);
        }

        public static void SendSMS(string to, string body)
        {
            string accountSid = "ACb1c92b3648039cc85e1bb7432b8d7194";
            string authToken = "dc3c0e820a35124022b2558adc87d2ef";

            TwilioClient.Init(accountSid, authToken);

            MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber("+14352535806"),
                to: new Twilio.Types.PhoneNumber("+55" + to)
            );
        }
    }
}
