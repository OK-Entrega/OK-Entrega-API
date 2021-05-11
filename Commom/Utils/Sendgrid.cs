using SendGrid;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace Commom.Utils
{
    public static class Sendgrid
    {
        public static async Task SendEmail(string to, string subject, string body)
        {
            var apiKey = "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";

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("daniel.amaral720@gmail.com", "OKEntrega"),
                Subject = subject,
                HtmlContent = $"{body}"
            };

            msg.AddTo(new EmailAddress(to));

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
