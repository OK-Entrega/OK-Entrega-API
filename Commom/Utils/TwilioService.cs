using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Commom.Utils
{
    public static class TwilioService
    {
        public static async Task SendSMS(string to, string body)
        {
            string accountSid = "ACb1c92b3648039cc85e1bb7432b8d7194";
            string authToken = "dc3c0e820a35124022b2558adc87d2ef";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber("+14352535806"),
                to: new Twilio.Types.PhoneNumber("+55"+to)
            );
        }
    }
}
